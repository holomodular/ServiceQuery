using System.Linq;
using System.Threading.Tasks;

namespace ServiceQuery
{
    /// <summary>
    /// Extensions for the ServiceQueryRequest object.
    /// </summary>
    public static class ServiceQueryRequestExtensions
    {
        /// <summary>
        /// Advanced Method. Get the ServiceQuery object.
        /// This allows access to advanced methods.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ServiceQueryException"></exception>
        public static ServiceQuery GetServiceQuery(this IServiceQueryRequest request)
        {
            var builder = new ServiceQueryBuilder();
            if (request.Filters == null || request.Filters.Count == 0)
                return builder.Build();
            foreach (var filter in request.Filters)
            {
                switch (filter.FilterType)
                {
                    case ServiceQueryServiceFilterType.and:
                        builder.And();
                        break;

                    case ServiceQueryServiceFilterType.average:
                        builder.Average(filter.Properties?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.begin:
                        builder.BeginExpression();
                        break;

                    case ServiceQueryServiceFilterType.between:
                        builder.Between(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault(),
                            filter.Values?.LastOrDefault());
                        break;

                    //case ServiceQueryServiceFilterType.binarychecksum:
                    //    builder.BinaryChecksum(filter.Properties?.FirstOrDefault());
                    //    break;

                    //case ServiceQueryServiceFilterType.checksum:
                    //    builder.Checksum(filter.Properties?.FirstOrDefault());
                    //    break;

                    case ServiceQueryServiceFilterType.contains:
                        builder.Contains(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.count:
                        builder.Count();
                        break;

                    case ServiceQueryServiceFilterType.distinct:
                        builder.Distinct();
                        break;

                    case ServiceQueryServiceFilterType.end:
                        builder.EndExpression();
                        break;

                    case ServiceQueryServiceFilterType.endswith:
                        builder.EndsWith(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.equal:
                        builder.IsEqual(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.greaterthan:
                        builder.IsGreaterThan(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.greaterthanorequal:
                        builder.IsGreaterThanOrEqual(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.includecount:
                        var includecount = filter.Values?.FirstOrDefault();
                        if (!string.IsNullOrEmpty(includecount))
                        {
                            bool tempbool;
                            if (bool.TryParse(includecount, out tempbool))
                                builder.IncludeCount = tempbool;
                            else
                                throw new ServiceQueryException("includecount is not a boolean");
                        }
                        else
                            builder.IncludeCount = true;
                        break;

                    case ServiceQueryServiceFilterType.inset:
                        builder.IsInSet(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.ToArray());
                        break;

                    case ServiceQueryServiceFilterType.isnull:
                        builder.IsNull(filter.Properties?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.isnotnull:
                        builder.IsNotNull(filter.Properties?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.lessthan:
                        builder.IsLessThan(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.lessthanorequal:
                        builder.IsLessThanOrEqual(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.maximum:
                        builder.Maximum(filter.Properties?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.minimum:
                        builder.Minimum(filter.Properties?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.notequal:
                        builder.IsNotEqual(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.notinset:
                        builder.IsNotInSet(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.ToArray());
                        break;

                    case ServiceQueryServiceFilterType.or:
                        builder.Or();
                        break;

                    case ServiceQueryServiceFilterType.pagenumber:
                        var pagenum = filter.Values?.FirstOrDefault();
                        if (!string.IsNullOrEmpty(pagenum))
                        {
                            int tempint;
                            if (int.TryParse(pagenum, out tempint))
                                builder.PageNumber = tempint;
                            else
                                throw new ServiceQueryException("pagenumber is not a number");
                        }
                        else
                            throw new ServiceQueryException("pagenumber must be a number");
                        break;

                    case ServiceQueryServiceFilterType.pagesize:
                        var pagesize = filter.Values?.FirstOrDefault();
                        if (!string.IsNullOrEmpty(pagesize))
                        {
                            int tempint;
                            if (int.TryParse(pagesize, out tempint))
                                builder.PageSize = tempint;
                            else
                                throw new ServiceQueryException("pagesize is not a number");
                        }
                        else
                            throw new ServiceQueryException("pagesize must be a number");
                        break;

                    case ServiceQueryServiceFilterType.select:
                        builder.Select(filter.Properties?.ToArray());
                        break;

                    case ServiceQueryServiceFilterType.startswith:
                        builder.StartsWith(filter.Properties?.FirstOrDefault(), filter.Values?.FirstOrDefault());
                        break;

                    case ServiceQueryServiceFilterType.sortasc:
                        builder.Sort(filter.Properties?.FirstOrDefault(), true);
                        break;

                    case ServiceQueryServiceFilterType.sortdesc:
                        builder.Sort(filter.Properties?.FirstOrDefault(), false);
                        break;

                    case ServiceQueryServiceFilterType.sum:
                        builder.Sum(filter.Properties?.FirstOrDefault());
                        break;
                }
            }

            return builder.Build();
        }

        /// <summary>
        /// Execute a Service Query and return a response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceQueryRequest"></param>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static ServiceQueryResponse<T> Execute<T>(this IServiceQueryRequest serviceQueryRequest, IQueryable<T> queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
            if (queryable == null)
                return null;

            var serviceQuery = serviceQueryRequest.GetServiceQuery();
            return serviceQuery.Execute<T>(queryable, serviceQueryOptions);
        }
    }
}