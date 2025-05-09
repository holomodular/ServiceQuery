﻿using System.Linq;
using System.Threading.Tasks;

namespace ServiceQuery
{
    /// <summary>
    /// Extensions for the ServiceQueryRequest object.
    /// </summary>
    public static class ServiceQueryRequestMongoDbExtensions
    {
        /// <summary>
        /// Execute a Service Query and return a response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceQueryRequest"></param>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static async Task<ServiceQueryResponse<T>> MongoDbExecuteAsync<T>(this IServiceQueryRequest serviceQueryRequest, IQueryable<T> queryable, ServiceQueryOptions serviceQueryOptions = null)
            where T : class
        {
            var serviceQuery = serviceQueryRequest.GetServiceQuery();
            return await serviceQuery.MongoDbExecuteAsync<T>(queryable, serviceQueryOptions);
        }
    }
}