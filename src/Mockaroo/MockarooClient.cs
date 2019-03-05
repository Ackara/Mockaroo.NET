using Acklann.Mockaroo.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

using System.Text;
using System.Threading.Tasks;

namespace Acklann.Mockaroo
{
    /// <summary>
    /// Provides methods to communicate with the Mockaroo REST API.
    /// </summary>
    public class MockarooClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooClient"/> class.
        /// </summary>
        public MockarooClient() : this(_defaultApiKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your API key.</param>
        public MockarooClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            _apiKey = apiKey ?? _defaultApiKey;
        }

        internal const int DEFAULT_LIMIT = 25;

        /// <summary>
        /// Sets the default api-key for all new instances of <see cref="MockarooClient"/>.
        /// </summary>
        /// <param name="secretKey">The secret key.</param>
        public static void SetApiKey(string secretKey)
        {
            _defaultApiKey = secretKey;
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <param name="endpoint">The Mockaroo endpoint.</param>
        /// <param name="schema">The Mockaroo schema.</param>
        /// <returns>Task&lt;System.Byte[]&gt;.</returns>
        public static async Task<byte[]> FetchDataAsync(string endpoint, Schema schema)
        {
            using (var http = new HttpClient())
            {
                string requestBody = schema.ToString();
                var response = await http.PostAsync(endpoint, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    string responseBody = await response?.Content?.ReadAsStringAsync();
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(responseBody);
#endif
                    responseBody = JObject.Parse(responseBody).Value<string>("error");
                    throw new HttpRequestException($"[{response.StatusCode}] {responseBody}.");
                }
            }
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="records">The number of records to retrieve.</param>
        /// <param name="format">The data format.</param>
        /// <returns>The raw data.</returns>
        public Task<byte[]> FetchDataAsync(Schema schema, int records = DEFAULT_LIMIT, Format format = Format.JSON)
        {
            return FetchDataAsync(Endpoint(records, format), schema);
        }

        // ==========

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="type">The return type.</param>
        /// <param name="records">The number of records to retrieve.</param>
        /// <returns>An array.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<object[]> FetchDataAsync(Schema schema, Type type, int records = DEFAULT_LIMIT)
        {
            byte[] data = await FetchDataAsync(Endpoint(records), schema);
            return MockarooConvert.FromJson(Encoding.UTF8.GetString(data), type);
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <param name="type">The data-template.</param>
        /// <param name="records">The number of records to retrieve.</param>
        /// <param name="depth">The max-depth the serializer should traverse down the object tree.</param>
        /// <returns>The array of <paramref name="type"/> objects.</returns>
        public async Task<object[]> FetchDataAsync(Type type, int records = DEFAULT_LIMIT, int depth = Schema.DEFAULT_DEPTH)
        {
            byte[] data = await FetchDataAsync(Endpoint(records), MockarooConvert.ToSchema(type, depth));
            return MockarooConvert.FromJson(Encoding.UTF8.GetString(data), type);
        }

        // ==========

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="schema">The schema.</param>
        /// <param name="records">The number of records to retrieve.</param>
        /// <returns>An array of <typeparamref name="T"/>.</returns>
        public async Task<T[]> FetchDataAsync<T>(Schema schema, int records = DEFAULT_LIMIT)
        {
            byte[] data = await FetchDataAsync(Endpoint(records), schema);
            return MockarooConvert.FromJson<T>(Encoding.UTF8.GetString(data));
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="records">The number of records to retrieve.</param>
        /// <param name="depth">The max-depth the serializer should traverse down the object tree.</param>
        /// <returns> The array of <typeparamref name="T" /> objects.</returns>
        public async Task<T[]> FetchDataAsync<T>(int records = DEFAULT_LIMIT, int depth = Schema.DEFAULT_DEPTH)
        {
            byte[] data = await FetchDataAsync(Endpoint(records), MockarooConvert.ToSchema(typeof(T), depth));
            return MockarooConvert.FromJson<T>(Encoding.UTF8.GetString(data));
        }

        // ==========

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com and save it to disk.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="schema">The schema.</param>
        /// <param name="take">The number of records to return.</param>
        /// <param name="records">The total number of records to retrieve and store on disk.</param>
        /// <returns></returns>
        public Task<T[]> FetchAndSaveDataAsync<T>(Schema schema, int take = DEFAULT_LIMIT, int records = (DEFAULT_LIMIT * 2))
        {
            return FetchAndSaveDataAsync<T>(schema, _defaultDirectory, take, records);
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com and save it to disk.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="take">The number of records to return.</param>
        /// <param name="records">The total number of records to retrieve and store on disk.</param>
        /// <param name="depth">The max-depth the serializer should traverse down the object tree.</param>
        /// <returns></returns>
        public async Task<T[]> FetchAndSaveDataAsync<T>(int take = DEFAULT_LIMIT, int records = (DEFAULT_LIMIT * 2), int depth = Schema.DEFAULT_DEPTH)
        {
            var schema = new Schema<T>(depth);
            return await FetchAndSaveDataAsync<T>(schema, _defaultDirectory, take, records);
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com and save it to disk.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="schema">The schema.</param>
        /// <param name="outputDirectory">The output directory.</param>
        /// <param name="take">The number of records to return.</param>
        /// <param name="records">The total number of records to retrieve and store on disk.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">outputDirectory</exception>
        public async Task<T[]> FetchAndSaveDataAsync<T>(Schema schema, string outputDirectory, int take = DEFAULT_LIMIT, int records = (DEFAULT_LIMIT * 2))
        {
            if (string.IsNullOrEmpty(outputDirectory)) throw new ArgumentNullException(nameof(outputDirectory));

            string dataFilePath = Path.Combine(outputDirectory, GenerateFileName(schema));
            if (File.Exists(dataFilePath))
            {
                T[] data = JsonConvert.DeserializeObject<T[]>(File.ReadAllText(dataFilePath));
                return data.Take(take).ToArray();
            }
            else
            {
                T[] data = await FetchDataAsync<T>(schema, records);
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                CreateDataFile(dataFilePath, json);
                return data.Take(take).ToArray();
            }
        }

        #region Private Members

        private static string _defaultApiKey;
        private readonly string _defaultDirectory = Path.Combine(Path.GetTempPath(), nameof(Mockaroo));
        private readonly string _apiKey;

        private static string GenerateFileName(Schema schema)
        {
            return $"{schema.ComputeChecksum()}.json";
        }

        private static void CreateDataFile(string filePath, string data)
        {
            string folder = Path.GetDirectoryName(filePath);
            if (Directory.Exists(folder) == false) Directory.CreateDirectory(folder);

            File.WriteAllText(filePath, data);
        }

        private string Endpoint(int records, Format format = Format.JSON)
        {
            return new UriBuilder("https", "www.mockaroo.com")
            {
                Path = $"api/generate.{format}".ToLower(),
                Query = $"key={_apiKey}&array=true&count={records}"
            }.ToString();
        }

        #endregion Private Members
    }
}