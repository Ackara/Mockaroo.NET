using Gigobyte.Mockaroo.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo
{
    public class MockarooClient
    {
        public MockarooClient(string apiKey) : this(apiKey, new ClrSchemaSerializer())
        {
        }

        public MockarooClient(string apiKey, ISchemaSerializer serializer)
        {
            _apiKey = apiKey;
            _serializer = serializer;
        }

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

        public IEnumerable<T> FetchData<T>(int records)
        {
            byte[] data = FetchDataAsync(Mockaroo.Endpoint(_apiKey, records), new Schema(typeof(T))).Result;
            return _serializer.ReadObject<T>(data);
        }

        public IEnumerable<T> FetchData<T>(Schema schema, int records)
        {
            byte[] data = FetchDataAsync(Mockaroo.Endpoint(_apiKey, records), schema).Result;
            return _serializer.ReadObject<T>(data);
        }

        public byte[] FetchData(Schema schema, int records, Format format = Format.JSON)
        {
            return FetchDataAsync(Mockaroo.Endpoint(_apiKey, records, format), schema).Result;
        }

        public async Task<IEnumerable<T>> FetchDataAsync<T>(int records)
        {
            byte[] data = await FetchDataAsync(Mockaroo.Endpoint(_apiKey, records), new Schema(typeof(T)));
            return _serializer.ReadObject<T>(data);
        }

        public async Task<IEnumerable<T>> FetchDataAsync<T>(Schema schema, int records)
        {
            byte[] data = await FetchDataAsync(Mockaroo.Endpoint(_apiKey, records), schema);
            return _serializer.ReadObject<T>(data);
        }

        public Task<byte[]> FetchDataAsync(Schema schema, int records, Format format = Format.JSON)
        {
            return FetchDataAsync(Mockaroo.Endpoint(_apiKey, records, format), schema);
        }

        #region Private Members

        private readonly string _apiKey;
        private readonly ISchemaSerializer _serializer;

        #endregion Private Members
    }
}