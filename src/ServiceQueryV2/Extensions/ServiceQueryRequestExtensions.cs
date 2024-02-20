using System.Dynamic;
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
                if (string.Compare(filter.FilterType, "and", true) == 0)
                {
                    builder.And();
                    continue;
                }
                if (string.Compare(filter.FilterType, "average", true) == 0)
                {
                    builder.Average(filter.Properties?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "begin", true) == 0)
                {
                    builder.Begin();
                    continue;
                }
                if (string.Compare(filter.FilterType, "between", true) == 0)
                {
                    builder.Between(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault(),
                        filter.Values?.LastOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "contains", true) == 0)
                {
                    builder.Contains(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "count", true) == 0)
                {
                    builder.Count();
                    continue;
                }
                if (string.Compare(filter.FilterType, "distinct", true) == 0)
                {
                    builder.Distinct();
                    continue;
                }
                if (string.Compare(filter.FilterType, "end", true) == 0)
                {
                    builder.End();
                    continue;
                }
                if (string.Compare(filter.FilterType, "endswith", true) == 0)
                {
                    builder.EndsWith(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "equal", true) == 0)
                {
                    builder.IsEqual(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "greaterthan", true) == 0)
                {
                    builder.IsGreaterThan(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "greaterthanorequal", true) == 0)
                {
                    builder.IsGreaterThanOrEqual(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "includecount", true) == 0)
                {
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
                    continue;
                }
                if (string.Compare(filter.FilterType, "inset", true) == 0)
                {
                    builder.IsInSet(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.ToArray());
                    continue;
                }
                if (string.Compare(filter.FilterType, "isnull", true) == 0)
                {
                    builder.IsNull(filter.Properties?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "isnotnull", true) == 0)
                {
                    builder.IsNotNull(filter.Properties?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "lessthan", true) == 0)
                {
                    builder.IsLessThan(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "lessthanorequal", true) == 0)
                {
                    builder.IsLessThanOrEqual(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "maximum", true) == 0)
                {
                    builder.Maximum(filter.Properties?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "minimum", true) == 0)
                {
                    builder.Minimum(filter.Properties?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "notequal", true) == 0)
                {
                    builder.IsNotEqual(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "notinset", true) == 0)
                {
                    builder.IsNotInSet(
                        filter.Properties?.FirstOrDefault(),
                        filter.Values?.ToArray());
                    continue;
                }
                if (string.Compare(filter.FilterType, "or", true) == 0)
                {
                    builder.Or();
                    continue;
                }
                if (string.Compare(filter.FilterType, "pagenumber", true) == 0)
                {
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
                    continue;
                }
                if (string.Compare(filter.FilterType, "pagesize", true) == 0)
                {
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
                    continue;
                }
                if (string.Compare(filter.FilterType, "select", true) == 0)
                {
                    builder.Select(filter.Properties?.ToArray());
                    continue;
                }
                if (string.Compare(filter.FilterType, "startswith", true) == 0)
                {
                    builder.StartsWith(filter.Properties?.FirstOrDefault(), filter.Values?.FirstOrDefault());
                    continue;
                }
                if (string.Compare(filter.FilterType, "sortasc", true) == 0)
                {
                    builder.Sort(filter.Properties?.FirstOrDefault(), true);
                    continue;
                }
                if (string.Compare(filter.FilterType, "sortdesc", true) == 0)
                {
                    builder.Sort(filter.Properties?.FirstOrDefault(), false);
                    continue;
                }
                if (string.Compare(filter.FilterType, "sum", true) == 0)
                {
                    builder.Sum(filter.Properties?.FirstOrDefault());
                    continue;
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