using System.Linq;

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
                string fexp = filter.FilterType?.ToLower();
                switch (fexp)
                {
                    default:
                        throw new ServiceQueryException($"filterType {filter.FilterType} not found");

                    case "and":
                    case "&":
                        builder.And();
                        break;

                    case "average":
                    case "avg":

                        builder.Average(filter.Properties?.FirstOrDefault());
                        break;

                    case "begin":
                    case "(":

                        builder.Begin();
                        break;

                    case "between":

                        builder.Between(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault(),
                            filter.Values?.LastOrDefault());
                        break;

                    case "contains":

                        builder.Contains(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case "count":

                        builder.Count();
                        break;

                    case "distinct":

                        builder.Distinct();
                        break;

                    case "end":
                    case ")":
                        builder.End();
                        break;

                    case "endswith":
                    case "ew":

                        builder.EndsWith(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case "equal":
                    case "eq":
                    case "=":

                        builder.IsEqual(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case "greaterthan":
                    case "gt":
                    case ">":

                        builder.IsGreaterThan(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case "greaterthanorequal":
                    case "greaterthanequal":
                    case "gtoe":
                    case "gte":
                    case ">=":

                        builder.IsGreaterThanOrEqual(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case "includecount":
                    case "ic":

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

                    case "inset":
                    case "set":

                        builder.IsInSet(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.ToArray());
                        break;

                    case "isnull":
                    case "null":

                        builder.IsNull(filter.Properties?.FirstOrDefault());
                        break;

                    case "isnotnull":
                    case "notnull":
                    case "!null":

                        builder.IsNotNull(filter.Properties?.FirstOrDefault());
                        break;

                    case "lessthan":
                    case "lt":
                    case "<":

                        builder.IsLessThan(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case "lessthanorequal":
                    case "lessthanequal":
                    case "ltoe":
                    case "lte":
                    case "<=":

                        builder.IsLessThanOrEqual(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case "maximum":
                    case "max":

                        builder.Maximum(filter.Properties?.FirstOrDefault());
                        break;

                    case "minimum":
                    case "min":

                        builder.Minimum(filter.Properties?.FirstOrDefault());
                        break;

                    case "notequal":
                    case "!=":

                        builder.IsNotEqual(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.FirstOrDefault());
                        break;

                    case "notinset":
                    case "notset":
                    case "!set":

                        builder.IsNotInSet(
                            filter.Properties?.FirstOrDefault(),
                            filter.Values?.ToArray());
                        break;

                    case "or":
                    case "|":

                        builder.Or();
                        break;

                    case "pagenumber":
                    case "pagenum":
                    case "pn":

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

                    case "pagesize":
                    case "ps":

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

                    case "select":
                    case "sel":

                        builder.Select(filter.Properties?.ToArray());
                        break;

                    case "startswith":
                    case "sw":

                        builder.StartsWith(filter.Properties?.FirstOrDefault(), filter.Values?.FirstOrDefault());
                        break;

                    case "sortasc":
                    case "sa":
                    case "orderasc":
                    case "oa":

                        builder.Sort(filter.Properties?.FirstOrDefault(), true);
                        break;

                    case "sortdesc":
                    case "sd":
                    case "orderdesc":
                    case "od":

                        builder.Sort(filter.Properties?.FirstOrDefault(), false);
                        break;

                    case "sum":

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