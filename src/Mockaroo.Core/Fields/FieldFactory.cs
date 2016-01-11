using System;
using System.Collections.Generic;

namespace Gigobyte.Mockaroo.Fields
{
    public partial class FieldFactory
    {
        public IEnumerable<IField> GetAllFields()
        {
            foreach (var type in _fieldTypes.Values)
            {
                IField field = (IField)Activator.CreateInstance(type);
                field.Name = type.Name;

                yield return field;
            }
        }
    }
}