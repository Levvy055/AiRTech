using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiRTech.Core.Math.Solvers.Math.Utils
{
    public class SimpleVector<T> : IEnumerable<T>
    {
        private readonly T[] _values;

        public T this[int key]
        {
            get
            {
                var v = default(T);
                if (_values.Length > key)
                {
                    v = _values[key];
                }
                return v;
            }
            set
            {
                if (_values.Length > key)
                {
                    _values[key] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Vector has size: {Size} while trying to assign {key} !");
                }
            }
        }

        internal SimpleVector(params T[] values)
        {
            _values = values;
        }

        internal SimpleVector(List<T> values) : this(values.ToArray())
        { }

        public T[] Values => _values;
        public int Size => Values?.Length ?? 0;
        public override string ToString()
        {
            var sb = new StringBuilder("[ ");
            for (var i = 0; i < Size; i++)
            {
                if (i > 0)
                {
                    sb.Append("; ");
                }
                var v = Values[i];
                sb.Append(v);
            }
            sb.Append(" ]");
            return sb.ToString();
        }

        public static double operator *(SimpleVector<T> v1, SimpleVector<T> v2)
        {
            if (v1.Size != v2.Size)
            {
                throw new ArgumentException($"The size of vectors to multiply are different! [{v1.Size}; {v2.Size}]");
            }
            double res = 0;
            for (var i = 0; i < v1.Size; i++)
            {
                res = Sum(res, Multiply(v1[i], v2[i]));
            }
            return res;
        }

        public static SimpleVector<T> operator *(SimpleVector<T> v, T d)
        {
            var list = new T[v.Size];
            for (var i = 0; i < v.Size; i++)
            {
                list[i] = Multiply(v[i], d);
            }
            return new SimpleVector<T>(list);
        }

        public static SimpleVector<T> operator *(T d, SimpleVector<T> v)
        {
            return v * d;
        }

        private static T Multiply(T a, T b)
        {
            return (dynamic)a * (dynamic)b;
        }

        private static double Sum(double a, T b)
        {
            return (dynamic)a + (dynamic)b;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.Select(a => a).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Round(int i)
        {
            for (var n = 0; n < Size; n++)
            {
                dynamic val = this[n];
                val = val * System.Math.Pow(10, i);
                val = System.Math.Round(val);
                val /= System.Math.Pow(10, i);
                this[n] = val;
            }
        }
    }
}