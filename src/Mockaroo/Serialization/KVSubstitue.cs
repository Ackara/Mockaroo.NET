namespace Acklann.Mockaroo.Serialization
{
    internal interface IKVSubstitue
    {
        object Key { get; }
        object Value { get; }
    }

    [System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "()}")]
    internal class KVSubstitue<K, V> : IKVSubstitue
    {
        public K Key;
        public V Value;

        object IKVSubstitue.Key => Key;

        object IKVSubstitue.Value => Value;

        protected internal virtual string GetDebuggerDisplay()
        {
            return $"{Key}: {Value}";
        }
    }
}