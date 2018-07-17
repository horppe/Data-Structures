using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            BiDictionary<string, string, int> biDic = new BiDictionary<string, string, int>();
            biDic.Add("Samson", "Opeyemi", 21);
            biDic.Add("Samson", "Opeyemi", 30);
            biDic.Add("Samson", "Opeyemi", 40);
            biDic.Add("Samson", "Opeyemi", 666);

            Console.ReadKey();
        }
    }

    class BiDictionary<Key1, Key2, Value>
    {
        public class Key3 : IComparable<Key3>
        {
            public Key1 firstKey;
            public Key2 secondKey;
            public Key3(Key1 key1, Key2 key2)
            {
                firstKey = key1;
                secondKey = key2;
            }
            public string Key
            {
                get { return firstKey.ToString() + secondKey.ToString(); }
            }

            public override bool Equals(object obj)
            {
                Key3 key = (Key3)obj;
                return this.Key.Equals(key.Key);
            }

            public override int GetHashCode()
            {
                return firstKey.GetHashCode() + secondKey.GetHashCode();
            }

            public override string ToString()
            {
                return firstKey.ToString() + secondKey.ToString();
            }

            public int CompareTo(Key3 key3)
            {
                return this.Key.CompareTo(key3.Key);
            }
        }
        private Dictionary<Key1, List<Value>> dictOne;
        private Dictionary<Key2, List<Value>> dictTwo;
        private Dictionary<Key3, List<Value>> dictThree;

        public BiDictionary()
        {
            dictOne = new Dictionary<Key1, List<Value>>();
            dictTwo = new Dictionary<Key2, List<Value>>();
            dictThree = new Dictionary<Key3, List<Value>>();
        }

        public void Add(Key1 keyOne, Key2 keyTwo, Value value)
        {
            List<Value> valOne;
            if (!dictOne.TryGetValue(keyOne, out valOne))
            {
                valOne = new List<Value>();
                dictOne[keyOne] = valOne;
            }
            valOne.Add(value);

            List<Value> valTwo;
            if (!dictTwo.TryGetValue(keyTwo, out valTwo))
            {
                valTwo = new List<Value>();
                dictTwo[keyTwo] = valTwo;
            }
            valTwo.Add(value);

            
            Key3 key = new Key3(keyOne, keyTwo);
            List<Value> valThree;
            if (!dictThree.TryGetValue(key, out valThree))
            {
                valThree = new List<Value>();
                dictThree[key] = valThree;
            }
            valThree.Add(value);

        }

        public void Add(Key1 keyOne, Value value)
        {
            dictOne.Add(keyOne, new List<Value>());
            dictOne[keyOne].Add(value);
        }

        public List<Value> GetValue(Key1 keyOne)
        {
            List<Value> val;
            if (dictOne.TryGetValue(keyOne, out val))
            {
                return val;
            }
            return null;
        }

        public List<Value> this[Key1 keyOne, Key2 keyTwo]
        {
            get
            {
                List<Value> val;
                Key3 key = new Key3(keyOne, keyTwo);
                if (dictThree.TryGetValue(key, out val))
                {
                    return val;
                }
                return null;
            }
        }
    }
}
