using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gigobyte.Mockaroo.Converters
{
    public interface IDataAdapter
    {
        object Convert(Stream stream);
    }
}
