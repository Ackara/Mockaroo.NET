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
        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooClient"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        public MockarooClient(string apiKey) : this(apiKey, new Serializer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MockarooClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your Mockaroo API key.</param>
        /// <param name="serializer">The serializer.</param>
        public MockarooClient(string apiKey, IMockarooSerializer serializer)
        {
            _mockarooApiKey = apiKey;
            _serializer = serializer;
        }

        /// <summary>
        /// Sends asynchronous request to http://mockaroo.com.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="records">The number of records to return.</param>
        /// <returns>A collection of <typeparamref name="T"/> objects.</returns>
        public async Task<IEnumerable<T>> FetchDataAsync<T>(int records)
        {
            return (await FetchDataAsync(typeof(T), new Schema(Schema.GetFields(typeof(T))), records)).Cast<T>();
        }

        /// <summary>
        /// Sends asynchronous request to http://mockaroo.com.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="schema">The Mockaroo schema used to generate the data.</param>
        /// <param name="records">The number of records to return.</param>
        /// <returns>A collection of <typeparamref name="T"/> objects.</returns>
        public async Task<IEnumerable<T>> FetchDataAsync<T>(Schema schema, int records)
        {
            return (await FetchDataAsync(typeof(T), schema, records)).Cast<T>();
        }

        /// <summary>
        /// Sends asynchronous request to http://mockaroo.com.
        /// </summary>
        /// <param name="returnType">The expected type of the object in collection.</param>
        /// <param name="records">The number of records to return.</param>
        /// <returns>A collection of <typeparamref name="T"/> objects.</returns>
        public async Task<IEnumerable<object>> FetchDataAsync(Type returnType, int records)
        {
            return await FetchDataAsync(returnType, new Schema(Schema.GetFields(returnType)), records);
        }

        /// <summary>
        /// Sends asynchronous request to http://mockaroo.com.
        /// </summary>
        /// <param name="returnType">The expected type of the object in collection.</param>
        /// <param name="schema">The Mockaroo schema used to generate the data.</param>
        /// <param name="records">The number of records to return.</param>
        /// <returns>A collection of <typeparamref name="T"/> objects.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public async Task<IEnumerable<object>> FetchDataAsync(Type returnType, Schema schema, int records)
        {
            var data = new LinkedList<object>();

            using (var client = new HttpClient())
            {
                string mockarooRestApiAddress = string.Format(_urlFormat, _mockarooApiKey, records, "json");
                string requestBody = schema.ToJson();

                using (var response = await client.PostAsync(mockarooRestApiAddress, new StringContent(requestBody)))
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

        /// <summary>
        /// Sends asynchronous request to http://mockaroo.com.
        /// </summary>
        /// <param name="schema">The Mockaroo schema used to generate the data.</param>
        /// <param name="records">The number of records to return.</param>
        /// <param name="format">The response format.</param>
        /// <returns>A collection of <typeparamref name="T"/> objects.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public async Task<System.IO.Stream> FetchDataAsync(Schema schema, int records, ResponseFormat format = ResponseFormat.JSON)
        {
            using (var client = new HttpClient())
            {
                string mockarooRestApiAddress = string.Format(_urlFormat, _mockarooApiKey, records, format).ToLower();
                string requestBody = schema.ToJson();

                var response = await client.PostAsync(mockarooRestApiAddress, new StringContent(requestBody));
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
                else throw new System.Net.WebException($"[Status={response.StatusCode}]: {response.ReasonPhrase}");
            }
        }

        #region Private Members

        private readonly string _mockarooApiKey;
        private readonly IMockarooSerializer _serializer;
        private readonly string _urlFormat = "http://www.mockaroo.com/api/generate.{2}?key={0}&count={1}&array=true";

        #endregion Private Members
    }
}