using Azure.Data.Tables;
using System.Threading.Tasks;

namespace ServiceQuery
{
    /// <summary>
    /// Extensions for the ServiceQueryRequest object.
    /// </summary>
    public static class ServiceQueryRequestAzureDataTablesExtensions
    {
        /// <summary>
        /// Execute a Service Query and return a response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceQueryRequest"></param>
        /// <param name="tableClient"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <param name="azureDataTablesOptions"></param>
        /// <returns></returns>
        public static ServiceQueryResponse<T> Execute<T>(this IServiceQueryRequest serviceQueryRequest, TableClient tableClient, ServiceQueryOptions serviceQueryOptions = null, AzureDataTablesOptions azureDataTablesOptions = null)
            where T : class, ITableEntity
        {
            if (tableClient == null)
                return null;

            var serviceQuery = serviceQueryRequest.GetServiceQuery();
            return serviceQuery.Execute<T>(tableClient, serviceQueryOptions, azureDataTablesOptions);
        }
    }
}