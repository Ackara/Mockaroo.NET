using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo
{
    [System.Diagnostics.DebuggerDisplay("{" + nameof(ToJson) + "()}")]
    public class RequestBody
    {

        #region Static Members
        public RequestBody Create(Type type)
        {
            throw new System.NotImplementedException();
        }

        public RequestBody Create<T>()
        {
            return Create(typeof(T));
        }

        #endregion
        public string Name { get; set; }

        public DataType Type { get; set; }

        public string ToJson()
        {
            throw new System.NotImplementedException();
        }
    }
}
