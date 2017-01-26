using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AiRTech.Core.Misc
{
    public class GenericDictionary<T> : IEnumerable where T : class
    {
        private readonly Dictionary<string, object> _dict = new Dictionary<string, object>();

        public object this[string key]
        {
            get
            {
                return _dict[key];
            }
            set
            {
                _dict[key] = value;
            }
        }

        public void Add(string key, T value)
        {
            _dict.Add(key, value);
        }

        public T GetValue(string key)
        {
            return _dict[key] as T;
        }

        public Dictionary<string, object> InnerDictionary => _dict;
        public int Size => _dict.Count;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public GenericDictionaryEnum GetEnumerator()
        {
            return new GenericDictionaryEnum(_dict.Values.ToArray());
        }

        public Dictionary<string, T> ToTypedDictionary()
        {
            return _dict.ToDictionary(v => v.Key, v => (T)v.Value);
        }
    }

    public class GenericDictionaryEnum : IEnumerator
    {
        public object[] Objects;
        private int _position = -1;

        public GenericDictionaryEnum(object[] objects)
        {
            Objects = objects;
        }

        public bool MoveNext()
        {
            _position++;
            return _position < Objects.Length;
        }

        public void Reset()
        {
            _position = -1;
        }

        object IEnumerator.Current => Current;

        public object Current
        {
            get
            {
                try
                {
                    return Objects[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
