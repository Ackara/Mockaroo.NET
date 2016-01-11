using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo
{
    public class MockarooClient
    {
        public MockarooClient(string apiKey) : this(apiKey, new Serializer())
        {
        }

        public MockarooClient(string apiKey, IMockarooSerializer serializer)
        {
            _apiKey = apiKey;
            _serializer = serializer;
        }

        public async Task<IEnumerable<T>> FetchDataAsync<T>(int records)
        {
            return (await FetchDataAsync(typeof(T), new Schema(Schema.GetFields(typeof(T))), records)).Cast<T>();
        }

        public async Task<IEnumerable<T>> FetchDataAsync<T>(Schema schema, int records)
        {
            return (await FetchDataAsync(typeof(T), schema, records)).Cast<T>();
        }

        public async Task<IEnumerable<object>> FetchDataAsync(Type returnType, int records)
        {
            return await FetchDataAsync(returnType, new Schema(Schema.GetFields(returnType)), records);
        }

        public async Task<IEnumerable<object>> FetchDataAsync(Type returnType, Schema schema, int records)
        {
            var data = new LinkedList<object>();

            using (var client = new HttpClient())
            {
                string url = string.Format(_urlFormat, _apiKey, records, "json");
                string requestBody = schema.ToJson();

                using (var response = await client.PostAsync(url, new StringContent(requestBody)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        foreach (JObject obj in JArray.Parse(json))
                        {
                            data.AddLast(_serializer.Deserialize(obj, returnType));
                        }
                    }
                    else throw new System.Net.WebException($"[Status={response.StatusCode}]: {response.ReasonPhrase}");
                }
            }
            return data;
        }

        public async Task<System.IO.Stream> FetchDataAsync(Schema schema, int records, ResponseFormat format = ResponseFormat.JSON)
        {
            using (var client = new HttpClient())
            {
                string url = string.Format(_urlFormat, _apiKey, records, format).ToLower();
                string requestBody = schema.ToJson();

                var response = await client.PostAsync(url, new StringContent(requestBody));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
                else throw new System.Net.WebException($"[Status={response.StatusCode}]: {response.ReasonPhrase}");
            }
        }

        #region Private Members

        private readonly string _apiKey;
        private readonly IMockarooSerializer _serializer;
        private readonly string _urlFormat = "http://www.mockaroo.com/api/generate.{2}?key={0}&count={1}&array=true";

        #endregion Private Members
    }
}