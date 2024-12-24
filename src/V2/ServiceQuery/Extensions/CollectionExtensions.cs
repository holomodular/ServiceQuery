#if NET40 || NET45 || NETSTANDARD2_0
#else
using System.Collections.Generic;
using System.Linq;
namespace ServiceQuery
{
    /// <summary>
    /// InMemory async support
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Async inmemory support
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IQueryable<T> AsAsyncInMemoryQueryable<T>(this ICollection<T> source)
        {
            return new AsyncInMemoryQueryable<T>(source.AsQueryable());
        }
    }
}
#endif