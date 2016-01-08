using System;
using System.Collections.Generic;

namespace Gigobyte.Mockaroo
{
    public class FieldFactory
    {
        public FieldFactory()
        {
            _fieldTypes = new Dictionary<DataType, Type>();
            LoadFieldTypes();
        }

        public IFieldInfo Create(DataType dataType)
        {
            try
            {
                Type fieldType = _fieldTypes[dataType];
                return (IFieldInfo)Activator.CreateInstance(fieldType);
            }
            catch (KeyNotFoundException ex)
            {
                throw new ArgumentException($"Cannot locate a <{nameof(IFieldInfo)}> associated with the {nameof(DataType)}:{dataType}.", nameof(dataType), ex);
            }
        }

        #region Private Member

        private IDictionary<DataType, Type> _fieldTypes;

        private void LoadFieldTypes()
        {
            throw new System.NotImplementedException();
        }

        #endregion Private Member
    }
}