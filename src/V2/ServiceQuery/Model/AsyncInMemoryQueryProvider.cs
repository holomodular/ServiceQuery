#if NET40 || NET45 || NET46 || NET47 || NET471 || NET472 || NET48 || NET481 || NETSTANDARD2_0
#else
using System.Linq;
using System.Linq.Expressions;

namespace ServiceQuery
{
    /// <summary>
    /// InMemory async query provider
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AsyncInMemoryQueryProvider<T> : IQueryProvider
    {
        private readonly IQueryProvider _source;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="source"></param>
        public AsyncInMemoryQueryProvider(IQueryProvider source)
        {
            _source = source;
        }

        /// <summary>
        /// Create query
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable CreateQuery(Expression expression)
        {
            return _source.CreateQuery(expression);
        }

        /// <summary>
        /// Create a query
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new AsyncInMemoryQueryable<TElement>(_source.CreateQuery<TElement>(expression));
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public object Execute(Expression expression)
        {
            return Execute<T>(expression);
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TResult Execute<TResult>(Expression expression)
        {
            return _source.Execute<TResult>(expression);
        }
    }
}
#endif