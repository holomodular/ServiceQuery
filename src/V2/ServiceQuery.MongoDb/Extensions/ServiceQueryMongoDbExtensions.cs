using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ServiceQuery
{
    /// <summary>
    /// Extensions for the ServiceQueryRequest object.
    /// </summary>
    public static class ServiceQueryMongoDbExtensions
    {
        /// <summary>
        /// Execute the Service Query and return a response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceQuery"></param>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static async Task<ServiceQueryResponse<T>> MongoDbExecuteAsync<T>(this IServiceQuery serviceQuery, IQueryable<T> queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
            try
            {
                if (queryable == null)
                    return null;

                if (serviceQuery.IsAggregate())
                {
                    var aggregate = await MongoDbExecuteAggregateAsync<T>(serviceQuery, queryable, serviceQueryOptions);
                    int? count = new Nullable<int>();
                    if (serviceQuery.IncludeCount)
                    {
                        var countquery = serviceQuery.Apply(queryable, serviceQueryOptions);
                        count = await countquery.CountAsync();
                    }
                    return new ServiceQueryResponse<T>()
                    {
                        Aggregate = aggregate,
                        Count = count,
                    };
                }

                var query = serviceQuery.Apply(queryable, serviceQueryOptions);
                if (serviceQuery.PageNumber > 0 && serviceQuery.PageSize > 0)
                {
                    int skip = ((serviceQuery.PageNumber - 1) * serviceQuery.PageSize);
                    return new ServiceQueryResponse<T>()
                    {
                        Count = serviceQuery.IncludeCount ? await query.CountAsync() : 0,
                        List = await query.Skip(skip).Take(serviceQuery.PageSize).ToListAsync()
                    };
                }
                else
                {
                    return new ServiceQueryResponse<T>()
                    {
                        Count = serviceQuery.IncludeCount ? await query.CountAsync() : 0
                    };
                }
            }
            catch (Exception ex)
            {
                throw new ServiceQueryException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Execute a Service Query and return the aggregate value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceQuery"></param>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        /// <exception cref="ServiceQueryException"></exception>
        public static async Task<double?> MongoDbExecuteAggregateAsync<T>(this IServiceQuery serviceQuery, IQueryable<T> queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
            try
            {
                if (serviceQuery == null || serviceQuery.Filters == null)
                    return null;

                var filter = serviceQuery.Filters.Where(x =>
                    x.FilterType == ServiceQueryFilterType.Aggregate).FirstOrDefault();
                if (filter == null)
                    return null;

                // If in-memory list, call sync version instead
                if (queryable is AsyncInMemoryQueryable<T>)
                    return serviceQuery.ExecuteAggregate<T>(queryable, serviceQueryOptions);

                //Get type and property info of the base type
                Type entityType = typeof(T);
                var properties = entityType.GetProperties();

                //Get column name mappings (in case storage object name is different than mapped object name)
                ServiceQueryOptions options = new ServiceQueryOptions();
                if (serviceQueryOptions != null)
                    options = serviceQueryOptions;
                Dictionary<string, string> mappings = options.PropertyNameMappings;
                if (mappings == null || mappings.Count == 0)
                {
                    mappings = new Dictionary<string, string>();
                    properties.ToList().ForEach(x => mappings.Add(x.Name, x.Name));
                    options.PropertyNameMappings = mappings;
                }
                var query = serviceQuery.Apply(queryable, options);

                // Count doesn't require a property selector
                if (filter.AggregateType == ServiceQueryAggregateType.Count)
                    return await query.CountAsync();

                var param = Expression.Parameter(entityType, "x");
                PropertyInfo prop = null;
                if (filter.Properties == null || filter.Properties.Count == 0)
                    throw new ServiceQueryException("requires at least 1 property");
                prop = ServiceQueryExtensions.GetProperty(properties, options, filter.Properties[0]);

                if (prop.PropertyType == typeof(bool))
                    throw new ServiceQueryException("aggregate functions not supported on bool");
                if (prop.PropertyType == typeof(bool?))
                    throw new ServiceQueryException("aggregate functions not supported on bool?");
                if (prop.PropertyType == typeof(byte))
                    throw new ServiceQueryException("aggregate functions not supported on byte");
                if (prop.PropertyType == typeof(byte?))
                    throw new ServiceQueryException("aggregate functions not supported on byte?");
                if (prop.PropertyType == typeof(byte[]))
                    throw new ServiceQueryException("aggregate functions not supported on byte[]");
                if (prop.PropertyType == typeof(byte?[]))
                    throw new ServiceQueryException("aggregate functions not supported on byte?[]");
                if (prop.PropertyType == typeof(char))
                    throw new ServiceQueryException("aggregate functions not supported on char");
                if (prop.PropertyType == typeof(char?))
                    throw new ServiceQueryException("aggregate functions not supported on char?");
#if NET6_0_OR_GREATER
                if (prop.PropertyType == typeof(DateOnly))
                    throw new ServiceQueryException("aggregate functions not supported on DateOnly");
                if (prop.PropertyType == typeof(DateOnly?))
                    throw new ServiceQueryException("aggregate functions not supported on DateOnly?");
#endif

                if (prop.PropertyType == typeof(DateTime))
                    throw new ServiceQueryException("aggregate functions not supported on DateTime");
                if (prop.PropertyType == typeof(DateTime?))
                    throw new ServiceQueryException("aggregate functions not supported on DateTime?");
                if (prop.PropertyType == typeof(DateTimeOffset))
                    throw new ServiceQueryException("aggregate functions not supported on DateTimeOffset");
                if (prop.PropertyType == typeof(DateTimeOffset?))
                    throw new ServiceQueryException("aggregate functions not supported on DateTimeOffset?");
#if NET6_0_OR_GREATER
                if (prop.PropertyType == typeof(TimeOnly))
                    throw new ServiceQueryException("aggregate functions not supported on TimeOnly");
                if (prop.PropertyType == typeof(TimeOnly?))
                    throw new ServiceQueryException("aggregate functions not supported on TimeOnly?");
#endif
                if (prop.PropertyType == typeof(TimeSpan))
                    throw new ServiceQueryException("aggregate functions not supported on TimeSpan");
                if (prop.PropertyType == typeof(TimeSpan?))
                    throw new ServiceQueryException("aggregate functions not supported on TimeSpan?");
                if (prop.PropertyType == typeof(decimal))
                {
                    var decimalSelector = Expression.Lambda<Func<T, decimal>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return Convert.ToDouble(await query.AverageAsync(decimalSelector));

                        case ServiceQueryAggregateType.Maximum:
                            return Convert.ToDouble(await query.MaxAsync(decimalSelector));

                        case ServiceQueryAggregateType.Minimum:
                            return Convert.ToDouble(await query.MinAsync(decimalSelector));

                        case ServiceQueryAggregateType.Sum:
                            return Convert.ToDouble(await query.SumAsync(decimalSelector));
                    }
                }
                if (prop.PropertyType == typeof(decimal?))
                {
                    var decimalNSelector = Expression.Lambda<Func<T, decimal?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            var aval = await query.AverageAsync(decimalNSelector);
                            if (aval.HasValue)
                                return Convert.ToDouble(aval.Value);
                            return null;

                        case ServiceQueryAggregateType.Maximum:
                            var maval = await query.MaxAsync(decimalNSelector);
                            if (maval.HasValue)
                                return Convert.ToDouble(maval.Value);
                            return null;

                        case ServiceQueryAggregateType.Minimum:
                            var mival = await query.MinAsync(decimalNSelector);
                            if (mival.HasValue)
                                return Convert.ToDouble(mival.Value);
                            return null;

                        case ServiceQueryAggregateType.Sum:
                            var sval = await query.SumAsync(decimalNSelector);
                            if (sval.HasValue)
                                return Convert.ToDouble(sval.Value);
                            return null;
                    }
                }
                if (prop.PropertyType == typeof(double))
                {
                    var doubleSelector = Expression.Lambda<Func<T, double>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return await query.AverageAsync(doubleSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(doubleSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(doubleSelector);

                        case ServiceQueryAggregateType.Sum:
                            return await query.SumAsync(doubleSelector);
                    }
                }
                if (prop.PropertyType == typeof(double?))
                {
                    var doubleNSelector = Expression.Lambda<Func<T, double?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return await query.AverageAsync(doubleNSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(doubleNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(doubleNSelector);

                        case ServiceQueryAggregateType.Sum:
                            return await query.SumAsync(doubleNSelector);
                    }
                }
                if (prop.PropertyType == typeof(Guid))
                    throw new ServiceQueryException("aggregate functions not supported on Guid");
                if (prop.PropertyType == typeof(Guid?))
                    throw new ServiceQueryException("aggregate functions not supported on Guid?");
                if (prop.PropertyType == typeof(short))
                {
                    var shortSelector = Expression.Lambda<Func<T, short>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on short");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(shortSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(shortSelector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on short");
                    }
                }
                if (prop.PropertyType == typeof(short?))
                {
                    var shortNSelector = Expression.Lambda<Func<T, short?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on short?");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(shortNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(shortNSelector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on short?");
                    }
                }
                if (prop.PropertyType == typeof(int))
                {
                    var intSelector = Expression.Lambda<Func<T, int>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return await query.AverageAsync(intSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(intSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(intSelector);

                        case ServiceQueryAggregateType.Sum:
                            return await query.SumAsync(intSelector);
                    }
                }
                if (prop.PropertyType == typeof(int?))
                {
                    var intNSelector = Expression.Lambda<Func<T, int?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return await query.AverageAsync(intNSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(intNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(intNSelector);

                        case ServiceQueryAggregateType.Sum:
                            return await query.SumAsync(intNSelector);
                    }
                }
                if (prop.PropertyType == typeof(long))
                {
                    var longSelector = Expression.Lambda<Func<T, long>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return await query.AverageAsync(longSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(longSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(longSelector);

                        case ServiceQueryAggregateType.Sum:
                            return await query.SumAsync(longSelector);
                    }
                }
                if (prop.PropertyType == typeof(long?))
                {
                    var longNSelector = Expression.Lambda<Func<T, long?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return await query.AverageAsync(longNSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(longNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(longNSelector);

                        case ServiceQueryAggregateType.Sum:
                            return await query.SumAsync(longNSelector);
                    }
                }
                if (prop.PropertyType == typeof(sbyte))
                {
                    var sbyteSelector = Expression.Lambda<Func<T, sbyte>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on sbyte");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(sbyteSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(sbyteSelector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on sbyte");
                    }
                }
                if (prop.PropertyType == typeof(sbyte?))
                {
                    var sbyteNSelector = Expression.Lambda<Func<T, sbyte?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on sbyte?");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(sbyteNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(sbyteNSelector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on sbyte?");
                    }
                }
                if (prop.PropertyType == typeof(Single))
                {
                    var singleSelector = Expression.Lambda<Func<T, Single>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return await query.AverageAsync(singleSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(singleSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(singleSelector);

                        case ServiceQueryAggregateType.Sum:
                            return await query.SumAsync(singleSelector);
                    }
                }
                if (prop.PropertyType == typeof(Single?))
                {
                    var singleNSelector = Expression.Lambda<Func<T, Single?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return await query.AverageAsync(singleNSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(singleNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(singleNSelector);

                        case ServiceQueryAggregateType.Sum:
                            return await query.SumAsync(singleNSelector);
                    }
                }
                if (prop.PropertyType == typeof(string))
                    throw new ServiceQueryException("aggregate functions not supported on string");
                if (prop.PropertyType == typeof(UInt16))
                {
                    var uint16Selector = Expression.Lambda<Func<T, UInt16>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on UInt16");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(uint16Selector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(uint16Selector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on UInt16");
                    }
                }
                if (prop.PropertyType == typeof(UInt16?))
                {
                    var uint16NSelector = Expression.Lambda<Func<T, UInt16?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on UInt16?");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(uint16NSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(uint16NSelector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on UInt16?");
                    }
                }
                if (prop.PropertyType == typeof(UInt32))
                {
                    var uint32Selector = Expression.Lambda<Func<T, UInt32>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on UInt32");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(uint32Selector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(uint32Selector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on UInt32");
                    }
                }
                if (prop.PropertyType == typeof(UInt32?))
                {
                    var uint32NSelector = Expression.Lambda<Func<T, UInt32?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on UInt32?");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(uint32NSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(uint32NSelector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on UInt32?");
                    }
                }
                if (prop.PropertyType == typeof(UInt64))
                {
                    var uint64Selector = Expression.Lambda<Func<T, UInt64>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on UInt64");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(uint64Selector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(uint64Selector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on UInt64");

                        default:
                            throw new ServiceQueryException("aggregate not defined");
                    }
                }
                if (prop.PropertyType == typeof(UInt64?))
                {
                    var uint64NSelector = Expression.Lambda<Func<T, UInt64?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on UInt64?");

                        case ServiceQueryAggregateType.Maximum:
                            return await query.MaxAsync(uint64NSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return await query.MinAsync(uint64NSelector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on UInt64?");
                    }
                }
#if NET7_0_OR_GREATER
                if (prop.PropertyType == typeof(UInt128))
                {
                    var uint128Selector = Expression.Lambda<Func<T, UInt128>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on UInt128");

                        case ServiceQueryAggregateType.Maximum:
                            return (double)await query.MaxAsync(uint128Selector);

                        case ServiceQueryAggregateType.Minimum:
                            return (double)await query.MinAsync(uint128Selector);

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on UInt128");
                    }
                }
                if (prop.PropertyType == typeof(UInt128?))
                {
                    var uint128NSelector = Expression.Lambda<Func<T, UInt128?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            throw new ServiceQueryException("average not supported on UInt128?");

                        case ServiceQueryAggregateType.Maximum:
                            var val = await query.MaxAsync(uint128NSelector);
                            if (val.HasValue)
                                return (double)val.Value;
                            return null;

                        case ServiceQueryAggregateType.Minimum:
                            var val2 = await query.MinAsync(uint128NSelector);
                            if (val2.HasValue)
                                return (double)val2.Value;
                            return null;

                        case ServiceQueryAggregateType.Sum:
                            throw new ServiceQueryException("sum not supported on UInt128?");
                    }
                }
#endif
                throw new ServiceQueryException("aggregate type not defined");
            }
            catch (Exception ex)
            {
                throw new ServiceQueryException(ex.Message + "-ExecuteAggregateAsync", ex);
            }
        }
    }
}