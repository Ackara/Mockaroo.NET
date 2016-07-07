using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo
{
    public class MockarooClient
    {
        public MockarooClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        #region Asynchronous Methods

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
                    throw new System.Net.WebException($"[Response={response.StatusCode}]: {responseBody}.");
                }
            }
        }

        #endregion Asynchronous Methods

        #region Private Members

        private readonly string _apiKey;

        #endregion Private Members
    }
}