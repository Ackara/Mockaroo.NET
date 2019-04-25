using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Acklann.Mockaroo
{
    /// <summary>
    /// Represents a collection of <typeparamref name="T"/> objects generated by &quot;https://mockaroo.com&quot;.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public class MockarooRepository<T> : IEnumerable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooRepository{T}"/> class.
        /// </summary>
        /// <param name="records">The number of records to download.</param>
        public MockarooRepository(int records) : this(null, records, null, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooRepository{T}"/> class.
        /// </summary>
        /// <param name="apiKey">The mockaroo API key.</param>
        /// <param name="records">The number records to download.</param>
        public MockarooRepository(string apiKey, int records) : this(apiKey, records, null, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooRepository{T}"/> class.
        /// </summary>
        /// <param name="apiKey">The mockaroo API key.</param>
        /// <param name="records">The number of records to download.</param>
        /// <param name="directory">The download directory.</param>
        /// <param name="autoSync">if set to <c>true</c> [automatic synchronize].</param>
        /// <param name="depth">The max-depth the serializer should traverse down the object tree.</param>
        public MockarooRepository(string apiKey, int records, string directory, bool autoSync = true, int depth = Mockaroo.Schema.DEFAULT_DEPTH)
        {
            _apiKey = apiKey;
            _records = records;
            _autoSync = autoSync;
            string folder = directory ?? Path.Combine(Path.GetTempPath(), nameof(Mockaroo));

            _schemaPath = Path.Combine(folder, typeof(T).FullName, "schema.json");
            _dataPath = Path.Combine(folder, typeof(T).FullName, "data.json");
            _fileHash = Helper.ComputeHash(_schemaPath);
            _schema = new Schema<T>(depth);
            _schema.Save(_schemaPath);
        }

        /// <summary>
        /// Gets the number of <typeparamref name="T"/> object in this instance.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get => _data?.Count ?? 0;
        }

        /// <summary>
        /// Gets a value indicating whether this instance <see cref="Schema"/> has not been changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [in synchronize]; otherwise, <c>false</c>.
        /// </value>
        public bool InSync
        {
            get { return _fileHash == Helper.ComputeHash(_schema); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to replace the current data with new data if the schema has changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if auto sync; otherwise, <c>false</c>.
        /// </value>
        public bool AutoSync
        {
            get => _autoSync;
            set { _autoSync = value; }
        }

        /// <summary>
        /// Gets the <see cref="Schema{T}"/>.
        /// </summary>
        /// <value>
        /// The schema.
        /// </value>
        public Schema<T> Schema
        {
            get => _schema;
        }

        /// <summary>
        /// Gets the <typeparamref name="T"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public T this[int index]
        {
            get
            {
                Load();
                if (_data == null || _data.Count == 0) return default(T);
                else if (index >= 0 && index < _data.Count) return default(T);
                else return _data[index];
            }
        }

        /// <summary>
        /// Write this instance <see cref="Schema"/> to disk.
        /// </summary>
        public void Save()
        {
            _schema.Save(_schemaPath);
        }

        /// <summary>
        /// Replace the current data with new data if the schema has changed.
        /// </summary>
        /// <returns></returns>
        public MockarooRepository<T> Sync()
        {
            if (!InSync) Refresh();
            return this;
        }

        /// <summary>
        /// Replace the current data with new data.
        /// </summary>
        public void Refresh()
        {
            var client = new MockarooClient(_apiKey);
            byte[] data = client.FetchDataAsync(_schema, _records).Result;
            string json = JArray.Parse(Encoding.UTF8.GetString(data)).ToString(Formatting.Indented);
            Helper.CreateDirectory(_dataPath);
            File.WriteAllText(_dataPath, json);

            _schema.Save(_schemaPath);
            _fileHash = Helper.ComputeHash(_schema);
            _data?.Clear();
        }

        /// <summary>
        /// Get a random <typeparamref name="T"/> instance from the underlying collection.
        /// </summary>
        /// <returns></returns>
        public T GetItem()
        {
            Load();
            return this[_random.Next(_data.Count)];
        }

        internal void Load()
        {
            if (_data == null || _data.Count == 0)
            {
                if (_autoSync) Sync();
                if (!File.Exists(_dataPath)) Refresh();

                using (var stream = File.OpenRead(_dataPath))
                {
                    _data = new List<T>(Serialization.MockarooConvert.FromJson<T>(stream));
                }
            }
        }

        #region IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            Load();
            return _data.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion IEnumerable

        #region Private Members

        private readonly int _records;
        private readonly Schema<T> _schema;
        private readonly Random _random = new Random();
        private readonly string _apiKey, _dataPath, _schemaPath;

        private IList<T> _data;
        private bool _autoSync;
        private string _fileHash;

        #endregion Private Members
    }
}