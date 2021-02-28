namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {

        private readonly Dictionary<Tuple<TKey1, TKey2>, TValue> values = new Dictionary<Tuple<TKey1, TKey2>, TValue>();

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements => this.values.Count;

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get { return this.values[Tuple.Create(key1, key2)]; }
            set { this.values[Tuple.Create(key1, key2)] = value; }
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            return this.values.Keys
                .Where(tuple => tuple.Item1.Equals(key1))
                .Select(tuple => Tuple.Create(tuple.Item2, this.values[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            return this.values.Keys
                .Where(tuple => tuple.Item2.Equals(key2))
                .Select(tuple => Tuple.Create(tuple.Item1, this.values[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            return this.values.Keys
                .Select(tuple => Tuple.Create(tuple.Item1, tuple.Item2, this.values[tuple]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            foreach (var k1 in keys1)
            {
                foreach (var k2 in keys2)
                {
                    this[k1, k2] = generator(k1, k2);
                }
            }
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(Map2D<TKey1, TKey2, TValue> other)
        {
            return Equals(values, other.values);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            if (other is Map2D<TKey1, TKey2, TValue> otherMap2d)
            {
                return this.Equals(otherMap2d);
            }
            return false;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Map2D<TKey1, TKey2, TValue>) obj);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return (values != null ? values.GetHashCode() : 0);
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            return $"{nameof(values)}: {values}";
        }
    }
}
