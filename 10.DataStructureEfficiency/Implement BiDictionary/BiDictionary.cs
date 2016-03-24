namespace ImplementBiDictionary
{
    using System;
    using System.Collections.Generic;

    public class BiDictionary<K1, K2, T>
    {
        private Dictionary<K1, List<T>> valuesByFirstKey = new Dictionary<K1, List<T>>();
        private Dictionary<K2, List<T>> valuesBySecondKey = new Dictionary<K2, List<T>>();
        private Dictionary<Tuple<K1, K2>, List<T>> valuesByBothKeys = new Dictionary<Tuple<K1, K2>, List<T>>();

        public int CountKey1 { get { return this.valuesByFirstKey.Count; } }
        public int CountKey2 { get { return this.valuesBySecondKey.Count; } }
        public int CountKey12 { get { return this.valuesByBothKeys.Count; } }

        public void Add(K1 key1, K2 key2, T value)
        {
            // Add new element in the first collection
            if (!this.valuesByFirstKey.ContainsKey(key1))
            {
                var newList = new List<T>();
                this.valuesByFirstKey.Add(key1, newList);
            }

            this.valuesByFirstKey[key1].Add(value);

            // Add new element in the second collection
            if (!this.valuesBySecondKey.ContainsKey(key2))
            {
                var newList = new List<T>();
                this.valuesBySecondKey.Add(key2, newList);
            }

            this.valuesBySecondKey[key2].Add(value);

            // Add new element in the third collection
            var combinedKey = Tuple.Create(key1, key2);
            // var combinedKey = new Tuple<K1, K2>(key1, key2);
            if (!this.valuesByBothKeys.ContainsKey(combinedKey))
            {
                var newList = new List<T>();
                this.valuesByBothKeys.Add(combinedKey, newList);
            }

            this.valuesByBothKeys[combinedKey].Add(value);
        }

        public bool Remove(K1 key1, K2 key2)
        {
            // Remove from valuesByBothKeys
            var combinedKey = Tuple.Create(key1, key2);
            var elements = this.valuesByBothKeys[combinedKey]; 
            if(elements.Count == 0)
            {
                return false;
            }

            foreach (var elementValue in elements)
            {
                this.valuesByFirstKey[key1].Remove(elementValue);
                this.valuesBySecondKey[key2].Remove(elementValue);
            }

            this.valuesByBothKeys.Remove(combinedKey);

            return true;
        }

        public IEnumerable<T> Find(K1 key1, K2 key2)
        {
            var combinedKey = Tuple.Create(key1, key2);
            List<T> elementsByBothKeys;
            this.valuesByBothKeys.TryGetValue(combinedKey, out elementsByBothKeys);
            if(elementsByBothKeys == null)
            {
                return new List<T>();
            }

            return elementsByBothKeys;
        }

        public IEnumerable<T> FindByKey1(K1 key1)
        {
            List<T> elementsByFirstkey;
            this.valuesByFirstKey.TryGetValue(key1, out elementsByFirstkey);
            if (elementsByFirstkey == null)
            {
                return new List<T>();
            }

            return elementsByFirstkey;
        }

        public IEnumerable<T> FindByKey2(K2 key2)
        {
            List<T> elementsBySecondKey;
            this.valuesBySecondKey.TryGetValue(key2, out elementsBySecondKey);
            if (elementsBySecondKey == null)
            {
                return new List<T>();
            }

            return elementsBySecondKey;
        }
    }
}
