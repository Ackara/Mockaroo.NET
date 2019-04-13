using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Acklann.Mockaroo
{
    public class MockarooRepository<T> : IEnumerable<T>
    {
        public MockarooRepository(string apiKey, int records) : this(apiKey, records, null)
        {
        }

        public MockarooRepository(string apiKey, int records, string filePath, int depth = Mockaroo.Schema.DEFAULT_DEPTH)
        {
            _apiKey = apiKey;
            _records = records;
            _dataPath = filePath ?? Path.Combine(Path.GetTempPath(), nameof(Mockaroo), $"{typeof(T).FullName}.json");
            _schemaPath = Path.ChangeExtension(_dataPath, ".schema.json");
            _hash = Helper.ComputeHash(_schemaPath);
            _schema = new Schema<T>(depth);
        }

        public bool InSync
        {
            get { return _hash == Helper.ComputeHash(_schema); }
        }

        public Schema<T> Schema
        {
            get => _schema;
        }

        public MockarooRepository<T> Sync()
        {
            if (!InSync) return RefreshAsync().Result;

            return this;
        }

        public async Task<MockarooRepository<T>> RefreshAsync()
        {
            var client = new MockarooClient(_apiKey);
            byte[] data = await client.FetchDataAsync(_schema, _records);
            Helper.CreateDirectory(_dataPath);
            File.WriteAllBytes(_dataPath, data);
            _schema.Save(_schemaPath);
            _hash = Helper.ComputeHash(_schema);
            _data?.Clear();

            return this;
        }

        internal void Load()
        {
            if (_data == null || _data.Count == 0)
            {
                if (!File.Exists(_dataPath)) throw new FileNotFoundException($"Could not find file at '{_dataPath}'.");
                using (var stream = File.OpenRead(_dataPath))
                {
                    _data = new List<T>(Serialization.MockarooConvert.FromJson<T>(stream));
                }
            }
        }

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            Load();
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion IEnumerable

        #region Private Members

        private readonly int _records;
        private readonly Schema<T> _schema;
        private readonly string _apiKey, _dataPath, _schemaPath;

        private string _hash;
        private ICollection<T> _data;

        #endregion Private Members
    }
}