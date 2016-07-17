using Gigobyte.Mockaroo.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo
{
    /// <summary>
    /// Provides methods to communicate with the Mockaroo REST API.
    /// </summary>
    public class MockarooClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your API key.</param>
        public MockarooClient(string apiKey) : this(apiKey, new ClrDataAdapter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your API key.</param>
        /// <param name="adapter">The serializer.</param>
        public MockarooClient(string apiKey, IDataAdapter adapter)
        {
            _apiKey = apiKey;
            _adapter = adapter;
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <param name="endpoint">The Mockaroo endpoint.</param>
        /// <param name="schema">The Mockaroo schema.</param>
        /// <returns>Task&lt;System.Byte[]&gt;.</returns>
        /// <exception cref="Exceptions.MockarooException"></exception>
        public static async Task<byte[]> FetchDataAsync(Uri endpoint, Schema schema)
        {
            using (var http = new HttpClient())
            {
                string requestBody = schema.ToJson();
                var response = await http.PostAsync(endpoint, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
                else
                {
                    string responseBody = await response?.Content?.ReadAsStringAsync();
                    responseBody = JObject.Parse(responseBody).Value<string>("error");
                    throw new Exceptions.MockarooException($"[{response.StatusCode}]: {responseBody}.");
                }
            }
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records">The number of records to retrieve.</param>
        /// <returns>A collection of the specified <typeparamref name="T"/>.</returns>
        public IEnumerable<T> FetchData<T>(int records)
        {
            byte[] data = FetchDataAsync(Mockaroo.Endpoint(_apiKey, records), new Schema(typeof(T))).Result;
            return ((object[])_adapter.Transform(data, typeof(T))).Cast<T>();
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="schema">The Mockaroo schema.</param>
        /// <param name="records">The number of records to retrieve.</param>
        /// <returns>A collection of the specified <typeparamref name="T"/>.</returns>
        public IEnumerable<T> FetchData<T>(Schema schema, int records)
        {
            byte[] data = FetchDataAsync(Mockaroo.Endpoint(_apiKey, records), schema).Result;
            return ((object[])_adapter.Transform(data, typeof(T))).Cast<T>();
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="records">The number of records to retrieve.</param>
        /// <param name="format">The response format.</param>
        /// <returns>The raw data.</returns>
        public byte[] FetchData(Schema schema, int records, Format format = Format.JSON)
        {
            return FetchDataAsync(Mockaroo.Endpoint(_apiKey, records, format), schema).Result;
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records">The number of records to retrieve.</param>
        /// <returns>A collection of the specified <typeparamref name="T"/>.</returns>
        public async Task<IEnumerable<T>> FetchDataAsync<T>(int records)
        {
            byte[] data = await FetchDataAsync(Mockaroo.Endpoint(_apiKey, records), new Schema(typeof(T)));
            return ((object[])_adapter.Transform(data, typeof(T))).Cast<T>();
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="schema">The schema.</param>
        /// <param name="records">The number of records to retrieve.</param>
        /// <returns>A collection of the specified of <typeparamref name="T"/>.</returns>
        public async Task<IEnumerable<T>> FetchDataAsync<T>(Schema schema, int records)
        {
            byte[] data = await FetchDataAsync(Mockaroo.Endpoint(_apiKey, records), schema);
            return ((object[])_adapter.Transform(data, typeof(T))).Cast<T>();
        }

        /// <summary>
        /// Retrieve sample data from http://mockaroo.com.
        /// </summary>
        /// <param name="schema">The Mockaroo schema.</param>
        /// <param name="records">The number of records to retrieve.</param>
        /// <param name="format">The response format.</param>
        /// <returns>The raw data.</returns>
        public Task<byte[]> FetchDataAsync(Schema schema, int records, Format format = Format.JSON)
        {
            return FetchDataAsync(Mockaroo.Endpoint(_apiKey, records, format), schema);
        }

        #region Private Members

        private readonly string _apiKey;
        private readonly IDataAdapter _adapter;

        #endregion Private Members
    }
}