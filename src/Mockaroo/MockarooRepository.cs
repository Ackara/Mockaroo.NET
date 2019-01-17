using Acklann.Mockaroo.Fields;
using Acklann.Mockaroo.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acklann.Mockaroo
{
    public class MockarooRepository
    {
        public MockarooRepository(string apiKey, int maxRecords)
        {
            _apiKey = apiKey;
            _totalRecords = maxRecords;
        }

        public IEnumerable<T> FetchPersistedData<T>(int records, Action<Schema<T>> modify, int depth = Schema.DEFAULT_DEPTH)
        {
            var schema = new Schema<T>(depth);
            modify(schema);

            var client = new MockarooClient(_apiKey);

            
            

            throw new System.NotImplementedException();
        }

        public IEnumerable<T> FetchPersistedData<T>(Schema schema, int records)
        {




            var client = new MockarooClient(_apiKey);




            throw new System.NotImplementedException();
        }



        #region Private Members
        private readonly string _apiKey;
        private readonly int _totalRecords;
        #endregion
    }
}
