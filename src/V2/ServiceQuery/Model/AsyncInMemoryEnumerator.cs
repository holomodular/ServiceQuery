#if NET40 || NET45 || NETSTANDARD2_0
#else
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceQuery
{
    /// <summary>
    /// InMemory async enumerator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AsyncInMemoryEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _source;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source"></param>
        public AsyncInMemoryEnumerator(IEnumerator<T> source)
        {
            _source = source;
        }

        /// <summary>
        /// Current
        /// </summary>
        public T Current
        {
            get { return _source.Current; }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <returns></returns>
        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.CompletedTask);
        }

        /// <summary>
        /// MoveNext
        /// </summary>
        /// <returns></returns>
        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_source.MoveNext());
        }
    }
}
#endif