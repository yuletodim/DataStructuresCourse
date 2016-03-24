namespace ImplementDictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomDictionary<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int InitialCapacity = 16;
        public const double LoadFactor = 0.75;
        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public CustomDictionary(int capacity = InitialCapacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public int Count { get; private set; }

        public int Capacity
        {
            get
            {
                return this.slots.Length;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return this.Select(element => element.Key);
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return this.Select(element => element.Value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }

            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            // I.Check LoadFactor
            this.GrowIfNecessary();

            // II.Find slot number by key
            int slotNumber = this.FindSlotNumber(key);

            // III.Check if the slot is empty
            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            // IV.Check for collision
            foreach (var element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException(
                        string.Format("Element with key {0} already exists.", key));
                }
            }

            // V.Create new element and add it last to the linked list in this slot
            var newElement = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newElement);

            // VI.Increase the count
            this.Count++;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            this.GrowIfNecessary();
            var slotNumber = this.FindSlotNumber(key);
            if(this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    element.Value = value;
                    return false;
                }
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newElement);
            this.Count++;
            return true;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int slotNumber = this.FindSlotNumber(key);
            var elements = this.slots[slotNumber];
            if(elements != null)
            {
                foreach (var element in elements)
                {
                    if (element.Key.Equals(key))
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        public TValue Get(TKey key)
        {
            var element = this.Find(key);
            if(element == null)
            {
                throw new KeyNotFoundException(
                    string.Format("Key {0} does not exist in the collection.", key));
            }

            return element.Value;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);
            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        public bool ContainsKey(TKey key)
        {
            var element = this.Find(key);
            return element != null;
        }

        public bool ContainsValue(TValue value)
        {
            var containsValue = false;
            foreach (var element in this)
            {
                if (element.Value.Equals(value))
                {
                    containsValue = true;
                    break;
                }
            }

            return containsValue;
        }

        public bool Remove(TKey key)
        {
            var slotNumber = this.FindSlotNumber(key);
            var currentElement = this.slots[slotNumber].First;
            while(currentElement != null)
            {
                if(currentElement.Value.Key.Equals(key))
                {
                    this.slots[slotNumber].Remove(currentElement);
                    this.Count--;
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
            this.Count = 0;
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var elements in this.slots)
            {
                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        yield return element;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int FindSlotNumber(TKey key)
        {
            var slotNumber = Math.Abs(key.GetHashCode()) % this.slots.Length;
            return slotNumber;
        }

        private void GrowIfNecessary()
        {
            var currentLoadFactor = (double)(this.Count + 1) / this.Capacity;
            if (currentLoadFactor > LoadFactor)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newDictionary = new CustomDictionary<TKey, TValue>(this.Capacity * 2);
            foreach (var element in this)
            {
                newDictionary.Add(element.Key, element.Value);
            }

            this.slots = newDictionary.slots;
        }
    }
}
