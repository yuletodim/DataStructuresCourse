using System;

namespace ImplementDictionary
{
    public class KeyValue<TKey, TValue>
    {
        public KeyValue(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public override bool Equals(object other)
        {
            var element = other as KeyValue<TKey, TValue>;
            var areEqual = this.Key.Equals(element.Key) && this.Value.Equals(element.Value);
            return areEqual;
        }

        public override int GetHashCode()
        {
            return this.CombineHashCode(this.Key.GetHashCode(), this.Value.GetHashCode());
        }

        private int CombineHashCode(int h1, int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}",
                this.Key.ToString(),
                this.Value.ToString());
        }
    }
}
