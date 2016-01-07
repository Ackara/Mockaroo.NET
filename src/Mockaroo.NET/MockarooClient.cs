using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<T>> FetchDataAsync<T>(int count)
        {
            string url = string.Format(_urlFormat, _apiKey, count);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent("[{\"name\": \"name\", \"type\": \"Full Name\" }]");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent("[{\"name\": \"name\", \"type\": \"Full Name\" }]"));
                System.Diagnostics.Debug.WriteLine(response.StatusCode);
                System.Diagnostics.Debug.WriteLine(response.Content.ToString());
            }

            throw new NotImplementedException();
        }

        #region Private Members
        
        private readonly string _apiKey;
        private readonly string _urlFormat = "http://www.mockaroo.com/api/generate.csv?key={0}&count={1}";

        #endregion
    }
}
