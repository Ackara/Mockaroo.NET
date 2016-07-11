using System.Collections;
using System.Collections.Generic;

namespace Gigobyte.Mockaroo.Fields
{
    public partial class JSONArrayField : ICollection<IField>
    {
        public JSONArrayField() : this(new IField[_defaultCapacity])
        {
        }

        public JSONArrayField(int capacity) : this(new IField[capacity])
        {
        }

        public JSONArrayField(params IField[] collection)
        {
            _count = 0;
            _items = collection;
        }

        public int Min { get; set; } = 1;

        public int Max { get; set; } = 5;

        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IField this[int index]
        {
            get { return _items[index]; }
        }

        public void Add(IField item)
        {
            Add(item, true);
        }

        public void Add(IField item, bool appendName)
        {
            if (appendName) item.Name = $"{Name}.{item.Name}";

            if (_count >= _items.Length) ResizeCollection();
            _items[_count++] = item;
        }

        public void Clear()
        {
            _count = 0;
            _items = new IField[_defaultCapacity];
        }

        public bool Contains(IField item)
        {
            for (int i = 0; i < _count; i++)
                if (_items[i] == null && item == null) { return true; }
                else if (_items[i] != null && _items[i].Equals(item)) { return true; }

            return false;
        }

        public void CopyTo(IField[] array, int arrayIndex)
        {
            int j = 0;
            for (int i = arrayIndex; i < _count; i++)
            {
                array[j++] = _items[i];
            }
        }

        public bool Remove(IField item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i] == null && item == null)
                {
                    ShiftCollection(i);
                    return true;
                }
                else if (_items[i] != null && (_items[i].Equals(item)))
                {
                    ShiftCollection(i);
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<IField> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToJson()
        {
            string childrenJson = "";
            for (int i = 0; i < _count; i++)
            {
                childrenJson += (_items[i].ToJson() + ",");
            }

            return $"{base.BaseJson()},\"minItems\":\"{Min}\",\"maxItems\":\"{Max}\"}},{childrenJson.TrimEnd(',')}";
        }

        #region Private Members

        private static readonly int _defaultCapacity = 4;

        private int _count;
        private IField[] _items;

        private void ShiftCollection(int index)
        {
            if (index == (_count - 1))
            {
                _items[index] = null;
            }
            else for (int i = index; i <= _count; i++)
                {
                    _items[i] = _items[i + 1];
                }

            _count--;
        }

        private void ResizeCollection()
        {
            int n = _count * 2;
            var newArray = new IField[n];
            CopyTo(newArray, 0);
            _items = newArray;
        }

        #endregion Private Members
    }
}