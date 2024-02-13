using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceQuery
{
    /// <summary>
    /// Extensions for the ServiceQueryRequest object.
    /// </summary>
    public static class ServiceQueryAzureDataTablesExtensions
    {
        /// <summary>
        /// Execute a Service Query and return a response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceQuery"></param>
        /// <param name="tableClient"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <param name="azureDataTablesOptions"></param>
        /// <returns></returns>
        public static ServiceQueryResponse<T> Execute<T>(this IServiceQuery serviceQuery, TableClient tableClient, ServiceQueryOptions serviceQueryOptions = null, AzureDataTablesOptions azureDataTablesOptions = null)
            where T : class, ITableEntity
        {
            if (tableClient == null)
                return null;

            AzureDataTablesOptions azdtOptions = new AzureDataTablesOptions();
            if (azureDataTablesOptions != null)
                azdtOptions = azureDataTablesOptions;

            ServiceQueryResponse<T> response = new ServiceQueryResponse<T>();
            try
            {
                Pageable<T> pagableResult;
                int pageSize = serviceQuery.PageSize;
                if (pageSize > 1000)
                    pageSize = 1000;

                // Determine if we need to download all records
                bool returnAllRecords = false;
                if (serviceQuery.Filters != null && serviceQuery.Filters.Where(x =>
                    x.FilterType == ServiceQueryFilterType.Compare &&
                    (x.CompareType == ServiceQueryCompareType.StartsWith ||
                     x.CompareType == ServiceQueryCompareType.EndsWith ||
                     x.CompareType == ServiceQueryCompareType.Contains)).Any())
                {
                    if (azdtOptions.DownloadAllRecordsForStringComparison)
                        returnAllRecords = true;
                }

                // Build expression we need for azure
                var selectProperties = serviceQuery.GetSelectProperties<T>(serviceQueryOptions);
                var whereExp = serviceQuery.BuildWhereExpression<T>(serviceQueryOptions);
                if (returnAllRecords)
                    whereExp = null;
                if (whereExp != null)
                    pagableResult = tableClient.Query<T>(whereExp, pageSize, selectProperties);
                else
                    pagableResult = tableClient.Query<T>(maxPerPage: pageSize, select: selectProperties);

                // Determine if we need to download all records for result set
                if (serviceQuery.Filters != null && serviceQuery.Filters.Where(x => x.FilterType == ServiceQueryFilterType.Aggregate).Any())
                {
                    if (azdtOptions.DownloadAllRecordsForAggregate)
                        returnAllRecords = true;
                }
                if (serviceQuery.Filters != null && serviceQuery.Filters.Where(x => x.FilterType == ServiceQueryFilterType.Distinct).Any())
                {
                    if (azdtOptions.DownloadAllRecordsForDistinct)
                        returnAllRecords = true;
                }
                if (serviceQuery.Filters != null && serviceQuery.Filters.Where(x => x.FilterType == ServiceQueryFilterType.Sort).Any())
                {
                    if (azdtOptions.DownloadAllRecordsForSort)
                        returnAllRecords = true;
                }
                if (serviceQuery.IncludeCount)
                {
                    if (azdtOptions.DownloadAllRecordsForCount)
                        returnAllRecords = true;
                }

                // Handle more that 1000 in PageSize
                int pageCount = 0;
                int recordCount = 0;
                long startRecordCount = (serviceQuery.PageNumber - 1) * serviceQuery.PageSize;
                long endRecordCount = serviceQuery.PageNumber * serviceQuery.PageSize;
                List<T> allRecords = new List<T>();
                foreach (Page<T> page in pagableResult.AsPages())
                {
                    pageCount++;
                    foreach (var item in page.Values)
                    {
                        recordCount++;
                        allRecords.Add(item);
                        if (recordCount > startRecordCount && recordCount <= endRecordCount)
                            response.List.Add(item);

                        if (recordCount > endRecordCount && !returnAllRecords)
                            break;
                    }
                    if (recordCount > endRecordCount && !returnAllRecords)
                        break;
                }
                if (serviceQuery.IncludeCount && returnAllRecords)
                    response.Count = recordCount;

                if (returnAllRecords)
                {
                    //figure out why again
                    if (serviceQuery.Filters != null && serviceQuery.Filters.Where(x =>
                    x.FilterType == ServiceQueryFilterType.Compare &&
                    (x.CompareType == ServiceQueryCompareType.StartsWith ||
                     x.CompareType == ServiceQueryCompareType.EndsWith ||
                     x.CompareType == ServiceQueryCompareType.Contains)).Any())
                    {
                        if (azdtOptions.DownloadAllRecordsForStringComparison)
                        {
                            var localquery = serviceQuery.Apply(allRecords.AsQueryable(), serviceQueryOptions);
                            response.List = localquery.ToList();
                            //Don't return here, keep going as other operations may rely on this
                        }
                    }

                    if (serviceQuery.Filters != null && serviceQuery.Filters.Where(x => x.FilterType == ServiceQueryFilterType.Aggregate).Any())
                    {
                        if (azdtOptions.DownloadAllRecordsForAggregate)
                        {
                            response.List = new List<T>(); //don't return any records
                            response.Aggregate = serviceQuery.ExecuteAggregate<T>(allRecords.AsQueryable(), serviceQueryOptions);
                            return response;
                        }
                    }
                    if (serviceQuery.Filters != null && serviceQuery.Filters.Where(x => x.FilterType == ServiceQueryFilterType.Distinct).Any())
                    {
                        if (azdtOptions.DownloadAllRecordsForDistinct)
                        {
                            var localquery = serviceQuery.Apply(allRecords.AsQueryable(), serviceQueryOptions);
                            response.List = localquery.ToList();
                            return response;
                        }
                    }
                    if (serviceQuery.Filters != null && serviceQuery.Filters.Where(x => x.FilterType == ServiceQueryFilterType.Sort).Any())
                    {
                        if (azdtOptions.DownloadAllRecordsForSort)
                        {
                            var localquery = serviceQuery.Apply(allRecords.AsQueryable(), serviceQueryOptions);
                            response.List = localquery.ToList();
                            return response;
                        }
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new ServiceQueryException(ex.Message, ex);
            }
        }
    }
}