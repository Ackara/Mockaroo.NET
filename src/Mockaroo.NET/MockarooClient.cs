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
                    using (var reader = new System.IO.StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        T record;
                        while (!reader.EndOfStream)
                        {
                            record = TypeLoader.LoadData<T>(reader.ReadLine());
                            data.AddLast(record);
                        }
                    }
                }
            }

            return data;
        }

        #region Private Members

        private readonly string _apiKey;
        private readonly string _urlFormat = "http://www.mockaroo.com/api/generate.csv?key={0}&count={1}";

        #endregion Private Members
    }
}