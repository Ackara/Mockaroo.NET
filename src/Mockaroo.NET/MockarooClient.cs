using System.Collections.Generic;
using System.Net.Http;
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
            var data = new LinkedList<T>();
            string url = string.Format(_urlFormat, _apiKey, count);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent("[{\"name\": \"name\", \"type\": \"Full Name\" }]"));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                }
            }

            return data;
        }

        #region Private Members

        private readonly string _apiKey;
        private readonly string _urlFormat = "http://www.mockaroo.com/api/generate.json?key={0}&count={1}&array=true";

        #endregion Private Members
    }
}