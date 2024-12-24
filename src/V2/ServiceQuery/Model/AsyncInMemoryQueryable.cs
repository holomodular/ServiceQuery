#if NET40 || NET45 || NETSTANDARD2_0
#else
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace ServiceQuery
{
    /// <summary>
    /// InMemory IQueryable supporting async
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncInMemoryQueryable<T> : IQueryable<T>, IOrderedQueryable<T>, IAsyncEnumerable<T>
    {
        private readonly IQueryable<T> _source;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source"></param>
        public AsyncInMemoryQueryable(IQueryable<T> source)
        {
            _source = source;
        }

        /// <summary>
        /// Element type
        /// </summary>
        public Type ElementType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// Expression
        /// </summary>
        public Expression Expression
        {
            get { return _source.Expression; }
        }

        /// <summary>
        /// Provider
        /// </summary>
        public IQueryProvider Provider
        {
            get { return new AsyncInMemoryQueryProvider<T>(_source.Provider); }
        }

        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _source.GetEnumerator();
        }

        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Get async enumerator
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new AsyncInMemoryEnumerator<T>(_source.GetEnumerator());
        }
    }
}
#endif