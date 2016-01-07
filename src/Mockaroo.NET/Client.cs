using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo
{
    public class Client
    {
        public Client(string apiKey)
        {
            _apiKey = apiKey;
        }

        public IEnumerable<T> FetchData<T>(int count)
        {
            string url = string.Format(_urlFormat, _apiKey, count);
            throw new NotImplementedException();
        }

        #region Private Members


        private readonly string _apiKey;
        private readonly string _urlFormat = "http://www.mockaroo.com/api/generate{0}?key={1}&count={2}";

        #endregion
    }
}
