﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ServiceQuery
{
    /// <summary>
    /// Extensions for the IServiceQuery interface
    /// </summary>
    public static partial class ServiceQueryExtensions
    {
        /// <summary>
        /// Apply the Service Query to the IQueryable object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static IQueryable<T> Apply<T>(this IServiceQuery query, IQueryable<T> queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
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
                //Use same property name (will be case-insensitive)
                mappings = new Dictionary<string, string>();
                properties.ToList().ForEach(x => mappings.Add(x.Name, x.Name));
                options.PropertyNameMappings = mappings;
            }

            //Get the filters back in grouped sets
            ServiceQueryFilterSet filterSet = GetFilterSet(query, options);

            //Build "where" clause
            queryable = BuildWhere<T>(queryable, filterSet, entityType, properties, options);

            //Build "select" clause
            queryable = BuildSelect<T>(queryable, filterSet, entityType, properties, options);

            //Add any "order by" clause
            return BuildOrderBy<T>(queryable, filterSet, entityType, properties, options);
        }

        private static IQueryable<T> BuildWhere<T>(this IQueryable<T> queryable, ServiceQueryFilterSet filterSet, Type entityType, PropertyInfo[] properties, ServiceQueryOptions serviceQueryOptions)
        {
            Expression<Func<T, bool>> lastSet = BuildWhereExpression<T>(
                filterSet, entityType, properties, serviceQueryOptions);
            if (lastSet != null)
                queryable = queryable.Where(lastSet);
            return queryable;
        }

        /// <summary>
        /// Advanced Method. Build the where expression predicate.
        /// Make sure to call ServiceQueryExpand() prior to this method if needed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> BuildWhereExpression<T>(this IServiceQuery query, ServiceQueryOptions serviceQueryOptions = null)
        {
            //Get type and property info of the base type
            Type entityType = typeof(T);
            var properties = entityType.GetProperties();

            //Get name mappings (in case storage object name is different than mapped object name)
            ServiceQueryOptions options = new ServiceQueryOptions();
            if (serviceQueryOptions != null)
                options = serviceQueryOptions;
            Dictionary<string, string> mappings = options.PropertyNameMappings;
            if (mappings == null || mappings.Count == 0)
            {
                // Use all existing property names
                mappings = new Dictionary<string, string>();
                properties.ToList().ForEach(x => mappings.Add(x.Name, x.Name));
                options.PropertyNameMappings = mappings;
            }

            //Get the filters back in grouped sets
            ServiceQueryFilterSet filterSet = GetFilterSet(query, options);

            //Build "where" clause
            return BuildWhereExpression<T>(filterSet, entityType, properties, options);
        }

        private static Expression<Func<T, bool>> BuildWhereExpression<T>(this ServiceQueryFilterSet filterSet, Type entityType, PropertyInfo[] properties, ServiceQueryOptions serviceQueryOptions)
        {
            try
            {
                Expression<Func<T, bool>> lastSet = null;
                bool nextSetExpressionAnd = true;
                Expression<Func<T, bool>> lastExpression = null;
                for (int allSets = 0; allSets < filterSet.WhereFilters.Count; allSets++)
                {
                    //Iterate through where filters
                    List<IServiceQueryFilter> filters = filterSet.WhereFilters[allSets];
                    bool nextExpressionAnd = true;
                    for (int curItem = 0; curItem < filters.Count; curItem++)
                    {
                        IServiceQueryFilter filter = filters[curItem];

                        //Get expression join type
                        if (filter.FilterType == ServiceQueryFilterType.Expression)
                        {
                            if (filter.ExpressionType == ServiceQueryExpressionType.And)
                            {
                                nextExpressionAnd = true;
                                if (filters.Count == 1)
                                    nextSetExpressionAnd = true;
                            }
                            else
                            {
                                nextExpressionAnd = false;
                                if (filters.Count == 1)
                                    nextSetExpressionAnd = false;
                            }
                            continue;
                        }
                        //Build new expression
                        Expression<Func<T, bool>> expr = BuildExpresion<T>(filter, entityType, properties, serviceQueryOptions);
                        if (expr == null)
                            continue;
                        if (lastExpression == null)
                        {
                            lastExpression = expr;
                            continue;
                        }

                        //Add to last expression based on expression join type
                        if (nextExpressionAnd)
                            lastExpression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr.Body, new ExpressionParameterReplacer(lastExpression.Parameters, expr.Parameters).Visit(lastExpression.Body)), expr.Parameters);
                        else
                            lastExpression = Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr.Body, new ExpressionParameterReplacer(lastExpression.Parameters, expr.Parameters).Visit(lastExpression.Body)), expr.Parameters);
                    }
                    if (lastExpression == null)
                        continue;
                    //Set last set to last expression
                    if (lastSet == null)
                    {
                        lastSet = lastExpression;
                        lastExpression = null;
                        continue;
                    }
                    //Build on last set with last expression
                    if (nextSetExpressionAnd)
                        lastSet = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(lastSet.Body, new ExpressionParameterReplacer(lastExpression.Parameters, lastSet.Parameters).Visit(lastExpression.Body)), lastSet.Parameters);
                    else
                        lastSet = Expression.Lambda<Func<T, bool>>(Expression.OrElse(lastSet.Body, new ExpressionParameterReplacer(lastExpression.Parameters, lastSet.Parameters).Visit(lastExpression.Body)), lastSet.Parameters);

                    lastExpression = null;
                }
                return lastSet;
            }
            catch (Exception ex)
            {
                throw new ServiceQueryException(ex.Message + "-WhereExpression", ex);
            }
        }

        /// <summary>
        /// Get a list of selected properties from the Service Query.
        /// If no properties are defined, it returns a null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static List<string> GetSelectProperties<T>(this IServiceQuery queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
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
                //Use same property name (will be case-insensitive)
                mappings = new Dictionary<string, string>();
                properties.ToList().ForEach(x => mappings.Add(x.Name, x.Name));
                options.PropertyNameMappings = mappings;
            }

            //Get the filters back in grouped sets
            ServiceQueryFilterSet filterSet = GetFilterSet(queryable, options);

            List<string> selectFilters = new List<string>();
            if (filterSet.SelectFilters != null && filterSet.SelectFilters.Count > 0)
            {
                foreach (var filter in filterSet.SelectFilters)
                {
                    if (filter.Properties != null && filter.Properties.Count > 0)
                    {
                        foreach (var prop in filter.Properties)
                        {
                            if (mappings.ContainsKey(prop))
                            {
                                var val = mappings[prop];
                                if (!selectFilters.Contains(val))
                                    selectFilters.Add(val);
                            }
                        }
                    }
                }
            }
            if (selectFilters.Count == 0)
                return null;
            return selectFilters;
        }

        /// <summary>
        /// Advanced Method. Build the select expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static Expression<Func<T, T>> BuildSelectExpression<T>(this IServiceQuery queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
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
                //Use same property name (will be case-insensitive)
                mappings = new Dictionary<string, string>();
                properties.ToList().ForEach(x => mappings.Add(x.Name, x.Name));
                options.PropertyNameMappings = mappings;
            }

            //Get the filters back in grouped sets
            ServiceQueryFilterSet filterSet = GetFilterSet(queryable, options);

            //Build "where" clause
            return BuildSelectExpressionEx<T>(filterSet, entityType, properties, options);
        }

        private static IQueryable<T> BuildSelect<T>(this IQueryable<T> query, ServiceQueryFilterSet filterSet, Type entityType, PropertyInfo[] properties, ServiceQueryOptions serviceQueryOptions)
        {
            try
            {
                bool addDistinct = false;
                List<string> selectList = new List<string>();
                for (int filterCount = 0; filterCount < filterSet.SelectFilters.Count; filterCount++)
                {
                    //Iterate through filters
                    IServiceQueryFilter filter = filterSet.SelectFilters[filterCount];
                    if (filter.FilterType == ServiceQueryFilterType.Distinct)
                    {
                        addDistinct = true;
                        continue;
                    }
                    if (filter.FilterType == ServiceQueryFilterType.Select)
                    {
                        if (filter.Properties == null || filter.Properties.Count == 0)
                            throw new ServiceQueryException("Select filter requires at least 1 field");
                        selectList.AddRange(filter.Properties);
                    }
                }

                //Add only selected columns (if specified)
                if (selectList.Count > 0)
                {
                    NewExpression newExp = Expression.New(typeof(T));
                    var typeParam = Expression.Parameter(entityType, "x");
                    List<MemberBinding> bindings = new List<MemberBinding>();
                    foreach (string item in selectList)
                    {
                        PropertyInfo p = GetProperty(properties, serviceQueryOptions, item);
                        MemberExpression memberExpression = Expression.Property(typeParam, p);
                        bindings.Add(Expression.Bind(p, memberExpression));
                    }

                    var init = Expression.MemberInit(newExp, bindings);
                    var lambda = Expression.Lambda<Func<T, T>>(init, typeParam);
                    if (addDistinct)
                        query = query.Select(lambda).Distinct();
                    else
                        query = query.Select(lambda);
                    return query;
                }

                //Add Distinct (if needed)
                if (addDistinct)
                    query = query.Distinct();

                return query;
            }
            catch (Exception ex)
            {
                throw new ServiceQueryException(ex.Message + "-Select", ex);
            }
        }

        private static Expression<Func<T, T>> BuildSelectExpressionEx<T>(ServiceQueryFilterSet filterSet, Type entityType, PropertyInfo[] properties, ServiceQueryOptions serviceQueryOptions)
        {
            try
            {
                List<string> selectList = new List<string>();
                for (int filterCount = 0; filterCount < filterSet.SelectFilters.Count; filterCount++)
                {
                    //Iterate through filters
                    IServiceQueryFilter filter = filterSet.SelectFilters[filterCount];
                    if (filter.FilterType == ServiceQueryFilterType.Distinct)
                        continue;
                    if (filter.FilterType == ServiceQueryFilterType.Select)
                    {
                        if (filter.Properties == null || filter.Properties.Count == 0)
                            throw new ServiceQueryException("Select filter requires at least 1 field");
                        selectList.AddRange(filter.Properties);
                    }
                }

                //Add only selected columns (if specified)
                if (selectList.Count > 0)
                {
                    NewExpression newExp = Expression.New(typeof(T));
                    var typeParam = Expression.Parameter(entityType, "x");
                    List<MemberBinding> bindings = new List<MemberBinding>();
                    foreach (string item in selectList)
                    {
                        PropertyInfo p = GetProperty(properties, serviceQueryOptions, item);
                        MemberExpression memberExpression = Expression.Property(typeParam, p);
                        bindings.Add(Expression.Bind(p, memberExpression));
                    }

                    var init = Expression.MemberInit(newExp, bindings);
                    var lambda = Expression.Lambda<Func<T, T>>(init, typeParam);
                    return lambda;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ServiceQueryException(ex.Message + "-SelectExpression", ex);
            }
        }

        private static Expression<Func<T, bool>> BuildExpresion<T>(this IServiceQueryFilter filter, Type t, PropertyInfo[] props, ServiceQueryOptions serviceQueryOptions)
        {
            switch (filter.FilterType)
            {
                //case ServiceQueryFilterType.Aggregate:
                //    break;
                case ServiceQueryFilterType.Between:
                    return GetBetweenExpression<T>(filter, t, props, serviceQueryOptions);

                case ServiceQueryFilterType.Compare:
                    return GetCompareExpression<T>(filter, t, props, serviceQueryOptions);
                //case ServiceQueryFilterType.Distinct:
                //    break; //No expression (handled in buildselect)
                //case ServiceQueryFilterType.Expression:
                //    break; //No expression (handled in buildwhere)
                case ServiceQueryFilterType.Null:
                    return GetNullExpression<T>(filter, t, props, serviceQueryOptions);
                //case ServiceQueryFilterType.PropertyCompare:
                //    break;
                //case ServiceQueryFilterType.Select:
                //    break; //No expression (handled in buildselect)
                case ServiceQueryFilterType.Set:
                    return GetSetExpression<T>(filter, t, props, serviceQueryOptions);
                    //case ServiceQueryFilterType.Sort:
                    //    break; //No expression (handled by buildorderby)
            }
            return null;
        }

        /// <summary>
        /// Get a property
        /// </summary>
        /// <param name="props"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ServiceQueryException"></exception>
        public static PropertyInfo GetProperty(PropertyInfo[] props, ServiceQueryOptions serviceQueryOptions, string name)
        {
            if (serviceQueryOptions.PropertyNameCaseSensitive)
            {
                if (!serviceQueryOptions.PropertyNameMappings.ContainsKey(name))
                    throw new ServiceQueryException("Property mapping not found for " + name);
                var p = props.Where(x => string.Compare(x.Name, serviceQueryOptions.PropertyNameMappings[name], true) == 0).FirstOrDefault();
                if (p == null)
                    throw new ServiceQueryException("Property not found for " + name);
                return p;
            }
            else
            {
                foreach (var key in serviceQueryOptions.PropertyNameMappings.Keys)
                {
                    if (key.ToLower() == name.ToLower())
                    {
                        var p = props.Where(x => string.Compare(x.Name, serviceQueryOptions.PropertyNameMappings[key], true) == 0).FirstOrDefault();
                        if (p == null)
                            throw new ServiceQueryException("Property not found for " + name);
                        return p;
                    }
                }
                throw new ServiceQueryException("Property not found for " + name);
            }
        }

        private static Expression<Func<T, bool>> GetSetExpression<T>(IServiceQueryFilter filter, Type t, PropertyInfo[] props, ServiceQueryOptions serviceQueryOptions)
        {
            if (filter.Properties == null || filter.Properties.Count != 1)
                throw new ServiceQueryException("Set requires 1 field");
            if (filter.Values == null || filter.Values.Count == 0)
                throw new ServiceQueryException("Set requires at least 1 value");

            //Use the comparison expression to build the expression.
            ServiceQueryBuilder builder = new ServiceQueryBuilder();
            foreach (string val in filter.Values)
            {
                if (filter.IncludeType == ServiceQueryIncludeType.Include)
                    builder.IsEqual(filter.Properties[0], val);
                else
                    builder.IsNotEqual(filter.Properties[0], val);
            }

            Expression<Func<T, bool>> exp = null;
            foreach (ServiceQueryFilter valFilter in builder.Filters)
            {
                Expression<Func<T, bool>> tempExp = GetCompareExpression<T>(valFilter, t, props, serviceQueryOptions);
                if (exp == null)
                    exp = tempExp;
                else
                {
                    if (filter.IncludeType == ServiceQueryIncludeType.Include)
                        exp = Expression.Lambda<Func<T, bool>>(Expression.OrElse(exp.Body, new ExpressionParameterReplacer(tempExp.Parameters, exp.Parameters).Visit(tempExp.Body)), exp.Parameters);
                    else
                        exp = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(exp.Body, new ExpressionParameterReplacer(tempExp.Parameters, exp.Parameters).Visit(tempExp.Body)), exp.Parameters);
                }
            }
            return exp;
        }

        private static Expression<Func<T, bool>> GetNullExpression<T>(IServiceQueryFilter filter, Type t, PropertyInfo[] props, ServiceQueryOptions serviceQueryOptions)
        {
            if (filter.Properties == null || filter.Properties.Count != 1)
                throw new ServiceQueryException("Null requires 1 field");

            PropertyInfo p = null;
            MemberExpression member = null;
            Expression[] constant = null;
            p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);

            if (p.PropertyType == typeof(string))
            {
                var arg = Expression.Parameter(t, "x");
                member = Expression.MakeMemberAccess(arg, p);
                BinaryExpression exp;
                if (filter.IncludeType == ServiceQueryIncludeType.Include)
                    exp = Expression.Equal(member, Expression.Constant(null, p.PropertyType));
                else
                    exp = Expression.NotEqual(member, Expression.Constant(null, p.PropertyType));
                return Expression.Lambda<Func<T, bool>>(exp, arg);
            }
            else
            {
                var typeParam = Expression.Parameter(t, "x");
                member = Expression.MakeMemberAccess(typeParam, p);
                var memberHasValue = Expression.PropertyOrField(member, "HasValue");
                //var memberHasValue = MemberExpression.Property(member, "HasValue");

                if (filter.IncludeType == ServiceQueryIncludeType.Include)
                    constant = new Expression[] { Expression.Constant(false) };
                else
                    constant = new Expression[] { Expression.Constant(true) };

                var equalsExp = Expression.Equal(memberHasValue, constant[0]);
                return Expression.Lambda(equalsExp, typeParam) as Expression<Func<T, bool>>;
            }
        }

        private static Expression<Func<T, bool>> GetBetweenExpression<T>(IServiceQueryFilter filter, Type t, PropertyInfo[] props, ServiceQueryOptions serviceQueryOptions)
        {
            if (filter.Properties == null || filter.Properties.Count != 1)
                throw new ServiceQueryException("Between comparison requires 1 field");
            if (filter.Values == null || filter.Values.Count != 2)
                throw new ServiceQueryException("Between comparison requires 2 values");

            //Use the comparison expression to build the between expression.
            ServiceQueryBuilder builder = new ServiceQueryBuilder();
            if (filter.IncludeType == ServiceQueryIncludeType.Include)
            {
                builder.IsGreaterThanOrEqual(filter.Properties[0], filter.Values[0]);
                builder.IsLessThanOrEqual(filter.Properties[0], filter.Values[1]);
            }
            else
            {
                builder.IsLessThan(filter.Properties[0], filter.Values[0]);
                builder.IsGreaterThan(filter.Properties[0], filter.Values[1]);
            }

            Expression<Func<T, bool>> first = GetCompareExpression<T>(builder.Filters[0], t, props, serviceQueryOptions);
            Expression<Func<T, bool>> second = GetCompareExpression<T>(builder.Filters[1], t, props, serviceQueryOptions);
            Expression<Func<T, bool>> exp = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(first.Body, new ExpressionParameterReplacer(second.Parameters, first.Parameters).Visit(second.Body)), first.Parameters);
            return exp;
        }

        internal class ExpressionParameterReplacer : ExpressionVisitor
        {
            public ExpressionParameterReplacer(IList<ParameterExpression> fromParameters, IList<ParameterExpression> toParameters)
            {
                ParameterReplacements = new Dictionary<ParameterExpression, ParameterExpression>();
                for (int i = 0; i != fromParameters.Count && i != toParameters.Count; i++)
                    ParameterReplacements.Add(fromParameters[i], toParameters[i]);
            }

            private IDictionary<ParameterExpression, ParameterExpression> ParameterReplacements
            {
                get;
                set;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                ParameterExpression replacement;
                if (ParameterReplacements.TryGetValue(node, out replacement))
                    node = replacement;
                return base.VisitParameter(node);
            }
        }

        private static readonly MethodInfo StringStartsWithMethod = typeof(string).GetMethod(@"StartsWith", new[] { typeof(string) });
        private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod(@"Contains", new[] { typeof(string) });
        private static readonly MethodInfo StringEndsWithMethod = typeof(string).GetMethod(@"EndsWith", new[] { typeof(string) });

        private static Expression<Func<T, bool>> GetCompareExpression<T>(IServiceQueryFilter filter, Type t, PropertyInfo[] props, ServiceQueryOptions serviceQueryOptions)
        {
            var typeParam = Expression.Parameter(t, "x");
            PropertyInfo p = null;
            MemberExpression member = null;
            Expression[] constant = null;
            switch (filter.CompareType)
            {
                case ServiceQueryCompareType.StartsWith:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("StartsWith comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("StartsWith comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(filter.Values[0]) };
                    var startsWithCall = Expression.Call(member, StringStartsWithMethod, constant);

                    if (p.PropertyType == typeof(string))
                    {
                        var second = Expression.Lambda(startsWithCall, typeParam) as Expression<Func<T, bool>>; ;
                        var arg = Expression.Parameter(t, "x");
                        member = Expression.MakeMemberAccess(arg, p);
                        BinaryExpression exp = Expression.NotEqual(member, Expression.Constant(null, p.PropertyType));
                        var first = Expression.Lambda<Func<T, bool>>(exp, arg);
                        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(first.Body, new ExpressionParameterReplacer(second.Parameters, first.Parameters).Visit(second.Body)), first.Parameters);
                    }

                    return Expression.Lambda(startsWithCall, typeParam) as Expression<Func<T, bool>>;

                case ServiceQueryCompareType.Contains:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("Contains comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("Contains comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(filter.Values[0]) };
                    var containsCall = Expression.Call(member, StringContainsMethod, constant);
                    if (p.PropertyType == typeof(string))
                    {
                        var second = Expression.Lambda(containsCall, typeParam) as Expression<Func<T, bool>>; ;
                        var arg = Expression.Parameter(t, "x");
                        member = Expression.MakeMemberAccess(arg, p);
                        BinaryExpression exp = Expression.NotEqual(member, Expression.Constant(null, p.PropertyType));
                        var first = Expression.Lambda<Func<T, bool>>(exp, arg);
                        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(first.Body, new ExpressionParameterReplacer(second.Parameters, first.Parameters).Visit(second.Body)), first.Parameters);
                    }
                    return Expression.Lambda(containsCall, typeParam) as Expression<Func<T, bool>>;

                case ServiceQueryCompareType.EndsWith:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("EndsWith comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("EndsWith comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(filter.Values[0]) };
                    var endsWithCall = Expression.Call(member, StringEndsWithMethod, constant);

                    if (p.PropertyType == typeof(string))
                    {
                        var second = Expression.Lambda(endsWithCall, typeParam) as Expression<Func<T, bool>>; ;
                        var arg = Expression.Parameter(t, "x");
                        member = Expression.MakeMemberAccess(arg, p);
                        BinaryExpression exp = Expression.NotEqual(member, Expression.Constant(null, p.PropertyType));
                        var first = Expression.Lambda<Func<T, bool>>(exp, arg);
                        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(first.Body, new ExpressionParameterReplacer(second.Parameters, first.Parameters).Visit(second.Body)), first.Parameters);
                    }
                    return Expression.Lambda(endsWithCall, typeParam) as Expression<Func<T, bool>>;

                case ServiceQueryCompareType.Equal:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("Equal comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("Equal comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var equalsExp = Expression.Equal(member, constant[0]);
                    return Expression.Lambda(equalsExp, typeParam) as Expression<Func<T, bool>>;

                case ServiceQueryCompareType.GreaterThan:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("GreaterThan comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("GreaterThan comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var greaterThanExp = Expression.GreaterThan(member, constant[0]);
                    return Expression.Lambda(greaterThanExp, typeParam) as Expression<Func<T, bool>>;

                case ServiceQueryCompareType.GreaterThanEqual:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("GreaterThanEqual comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("GreaterThanEqual comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var greaterThanEqualExp = Expression.GreaterThanOrEqual(member, constant[0]);
                    return Expression.Lambda(greaterThanEqualExp, typeParam) as Expression<Func<T, bool>>;

                case ServiceQueryCompareType.LessThan:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("LessThan comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("LessThan comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var lessThanExp = Expression.LessThan(member, constant[0]);
                    return Expression.Lambda(lessThanExp, typeParam) as Expression<Func<T, bool>>;

                case ServiceQueryCompareType.LessThanEqual:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("LessThanEqual comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("LessThanEqual comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var lessThanEqualExp = Expression.LessThanOrEqual(member, constant[0]);
                    return Expression.Lambda(lessThanEqualExp, typeParam) as Expression<Func<T, bool>>;

                case ServiceQueryCompareType.NotEqual:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new ServiceQueryException("NotEqual comparison requires 1 field");
                    p = GetProperty(props, serviceQueryOptions, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new ServiceQueryException("NotEqual comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var noteEqualsExp = Expression.NotEqual(member, constant[0]);
                    return Expression.Lambda(noteEqualsExp, typeParam) as Expression<Func<T, bool>>;
            }
            return null;
        }

        /// <summary>
        /// Advanced Method. Build the orderby expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceQuery"></param>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static Expression<Func<T, object>> BuildOrderByExpression<T>(this IServiceQuery serviceQuery, IQueryable<T> queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
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
                //Use same property name (will be case-insensitive)
                mappings = new Dictionary<string, string>();
                properties.ToList().ForEach(x => mappings.Add(x.Name, x.Name));
                options.PropertyNameMappings = mappings;
            }

            //Get the filters back in grouped sets
            ServiceQueryFilterSet filterSet = GetFilterSet(serviceQuery, options);

            //Build "where" clause
            return BuildOrderByExpression<T>(filterSet, entityType, properties, options);
        }

        private static Expression<Func<T, object>> BuildOrderByExpression<T>(ServiceQueryFilterSet filterSet, Type entityType, PropertyInfo[] properties, ServiceQueryOptions serviceQueryOptions)
        {
            try
            {
                if (filterSet.SortFilters.Count == 0)
                    return null;

                for (int i = 0; i < filterSet.SortFilters.Count; i++)
                {
                    foreach (string column in filterSet.SortFilters[i].Properties)
                    {
                        var type = typeof(T);
                        var property = type.GetProperty(column);
                        var parameter = Expression.Parameter(type, "x");
                        var access = Expression.Property(parameter, property);
                        var convert = Expression.Convert(access, typeof(object));
                        return Expression.Lambda<Func<T, object>>(convert, parameter);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ServiceQueryException(ex.Message + "-OrderByExpression", ex);
            }
        }

        private static IQueryable<T> BuildOrderBy<T>(this IQueryable<T> queryable, ServiceQueryFilterSet filterSet, Type entityType, PropertyInfo[] properties, ServiceQueryOptions serviceQueryOptions)
        {
            try
            {
                if (filterSet.SortFilters.Count == 0)
                    return queryable;

                var param = Expression.Parameter(entityType, "x");
                for (int i = 0; i < filterSet.SortFilters.Count; i++)
                {
                    foreach (string column in filterSet.SortFilters[i].Properties)
                    {
                        var p = GetProperty(properties, serviceQueryOptions, column);
                        if (p.PropertyType == typeof(bool))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, bool>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, bool>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(bool?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, bool?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, bool?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(byte))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, byte>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, byte>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(byte?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, byte?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, byte?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(char))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, char>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, char>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(char?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, char?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, char?>>(Expression.Property(param, p), param));
                        }
#if NET6_0_OR_GREATER
                        if (p.PropertyType == typeof(DateOnly))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, DateOnly>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, DateOnly>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(DateOnly?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, DateOnly?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, DateOnly?>>(Expression.Property(param, p), param));
                        }
#endif
                        if (p.PropertyType == typeof(DateTime))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, DateTime>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, DateTime>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(DateTime?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, DateTime?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, DateTime?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(DateTimeOffset))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, DateTimeOffset>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, DateTimeOffset>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(DateTimeOffset?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, DateTimeOffset?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, DateTimeOffset?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(decimal))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, decimal>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, decimal>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(decimal?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, decimal?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, decimal?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(double))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, double>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, double>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(double?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, double?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, double?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(Guid))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, Guid>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, Guid>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(Guid?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, Guid?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, Guid?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(short))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, short>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, short>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(short?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, short?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, short?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(int))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, int>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, int>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(int?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, int?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, int?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(long))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, long>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, long>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(long?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, long?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, long?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(sbyte))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, sbyte>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, sbyte>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(sbyte?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, sbyte?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, sbyte?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(Single))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, Single>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, Single>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(Single?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, Single?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, Single?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(string))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, string>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, string>>(Expression.Property(param, p), param));
                        }
#if NET6_0_OR_GREATER
                        if (p.PropertyType == typeof(TimeOnly))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, TimeOnly>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, TimeOnly>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(TimeOnly?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, TimeOnly?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, TimeOnly?>>(Expression.Property(param, p), param));
                        }
#endif
                        if (p.PropertyType == typeof(TimeSpan))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, TimeSpan>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, TimeSpan>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(TimeSpan?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, TimeSpan?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, TimeSpan?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(UInt16))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, UInt16>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, UInt16>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(UInt16?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, UInt16?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, UInt16?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(UInt32))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, UInt32>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, UInt32>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(UInt32?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, UInt32?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, UInt32?>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(UInt64))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, UInt64>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, UInt64>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(UInt64?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, UInt64?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, UInt64?>>(Expression.Property(param, p), param));
                        }
#if NET7_0_OR_GREATER
                        if (p.PropertyType == typeof(UInt128))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, UInt128>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, UInt128>>(Expression.Property(param, p), param));
                        }
                        if (p.PropertyType == typeof(UInt128?))
                        {
                            if (filterSet.SortFilters[i].SortType == ServiceQuerySortType.Ascending)
                                queryable = queryable.OrderBy(Expression.Lambda<Func<T, UInt128?>>(Expression.Property(param, p), param));
                            else
                                queryable = queryable.OrderByDescending(Expression.Lambda<Func<T, UInt128?>>(Expression.Property(param, p), param));
                        }
#endif
                    }
                }
                return queryable;
            }
            catch (Exception ex)
            {
                throw new ServiceQueryException(ex.Message + "-OrderBy", ex);
            }
        }

        private static ServiceQueryFilterSet GetFilterSet(this IServiceQuery query, ServiceQueryOptions options)
        {
            if (query.Filters != null && options != null && !options.AllowMissingExpressions)
                ValidateQuery(query);

            ServiceQueryFilterSet filterSet = new ServiceQueryFilterSet();
            List<IServiceQueryFilter> currentWhereFilters = new List<IServiceQueryFilter>();
            bool nestedExpression = false;
            for (int i = 0; i < query.Filters.Count; i++)
            {
                IServiceQueryFilter filter = query.Filters[i];
                switch (filter.FilterType)
                {
                    case ServiceQueryFilterType.Between:
                        currentWhereFilters.Add(filter);
                        break;

                    case ServiceQueryFilterType.Compare:
                        currentWhereFilters.Add(filter);
                        break;

                    case ServiceQueryFilterType.Distinct:
                        filterSet.SelectFilters.Add(filter);
                        break;

                    case ServiceQueryFilterType.Expression:
                        if (filter.ExpressionType == ServiceQueryExpressionType.Begin)
                        {
                            nestedExpression = true;
                            if (currentWhereFilters.Count > 0)
                            {
                                filterSet.WhereFilters.Add(currentWhereFilters);
                                currentWhereFilters = new List<IServiceQueryFilter>();
                            }
                            continue;
                        }
                        if (filter.ExpressionType == ServiceQueryExpressionType.End)
                        {
                            nestedExpression = false;
                            if (currentWhereFilters.Count > 0)
                            {
                                filterSet.WhereFilters.Add(currentWhereFilters);
                                currentWhereFilters = new List<IServiceQueryFilter>();
                            }
                            continue;
                        }
                        if (nestedExpression)
                            currentWhereFilters.Add(filter);
                        else
                        {
                            if (currentWhereFilters.Count > 0)
                            {
                                filterSet.WhereFilters.Add(currentWhereFilters);
                                currentWhereFilters = new List<IServiceQueryFilter>();
                            }
                            currentWhereFilters.Add(filter);
                            filterSet.WhereFilters.Add(currentWhereFilters);
                            currentWhereFilters = new List<IServiceQueryFilter>();
                        }
                        break;

                    case ServiceQueryFilterType.Null:
                        currentWhereFilters.Add(filter);
                        break;

                    case ServiceQueryFilterType.Select:
                        filterSet.SelectFilters.Add(filter);
                        break;

                    case ServiceQueryFilterType.Set:
                        currentWhereFilters.Add(filter);
                        break;

                    case ServiceQueryFilterType.Sort:
                        filterSet.SortFilters.Add(filter);
                        break;
                }
            }
            if (currentWhereFilters.Count > 0)
                filterSet.WhereFilters.Add(currentWhereFilters);

            return filterSet;
        }

        private static void ValidateQuery(IServiceQuery query)
        {
            var beginCount = query.Filters.Where(x => x.ExpressionType == ServiceQueryExpressionType.Begin).Count();
            var endCount = query.Filters.Where(x => x.ExpressionType == ServiceQueryExpressionType.End).Count();
            if (beginCount != endCount)
                throw new ServiceQueryException("Begin and End expression counts do not match");

            // Make sure all filters have an associated expression (if needed)
            bool anyWhereExpressionFound = false;
            bool isAndOrExpressionNeeded = false;
            Stack<ServiceQueryExpressionType> beginendstack = new Stack<ServiceQueryExpressionType>();
            foreach (var item in query.Filters)
            {
                switch (item.FilterType)
                {
                    case ServiceQueryFilterType.Between:
                        if (isAndOrExpressionNeeded)
                            throw new ServiceQueryException("Expression is needed before Between filter");
                        isAndOrExpressionNeeded = true;
                        anyWhereExpressionFound = true;
                        continue;

                    case ServiceQueryFilterType.Compare:
                        if (isAndOrExpressionNeeded)
                            throw new ServiceQueryException("Expression is needed before Compare filter");
                        isAndOrExpressionNeeded = true;
                        anyWhereExpressionFound = true;
                        continue;

                    case ServiceQueryFilterType.Null:
                        if (isAndOrExpressionNeeded)
                            throw new ServiceQueryException("Expression is needed before Null filter");
                        isAndOrExpressionNeeded = true;
                        anyWhereExpressionFound = true;
                        continue;

                    case ServiceQueryFilterType.Set:
                        if (isAndOrExpressionNeeded)
                            throw new ServiceQueryException("Expression is needed before Set filter");
                        isAndOrExpressionNeeded = true;
                        anyWhereExpressionFound = true;
                        continue;

                    case ServiceQueryFilterType.Expression:
                        switch (item.ExpressionType)
                        {
                            case ServiceQueryExpressionType.Begin:
                                beginendstack.Push(ServiceQueryExpressionType.Begin);
                                if (isAndOrExpressionNeeded)
                                    isAndOrExpressionNeeded = false;
                                continue;

                            case ServiceQueryExpressionType.End:
                                if (beginendstack.Count == 0)
                                    throw new ServiceQueryException("End Expression is before Begin");
                                beginendstack.Pop();
                                if (isAndOrExpressionNeeded)
                                    isAndOrExpressionNeeded = false;
                                isAndOrExpressionNeeded = true;
                                continue;

                            case ServiceQueryExpressionType.And:
                                if (isAndOrExpressionNeeded)
                                    isAndOrExpressionNeeded = false;
                                else
                                    throw new ServiceQueryException("And Expression is not needed");
                                continue;

                            case ServiceQueryExpressionType.Or:
                                if (isAndOrExpressionNeeded)
                                    isAndOrExpressionNeeded = false;
                                else
                                    throw new ServiceQueryException("Or Expression is not needed");
                                continue;
                        }
                        continue;
                        // ALl others don't matter
                }
            }
            if (anyWhereExpressionFound && !isAndOrExpressionNeeded)
                throw new Exception("Ending expression is missing");
        }

        private static object GetParsedPropertyType(PropertyInfo prop, string value)
        {
            if (prop.PropertyType == typeof(bool))
                return bool.Parse(value);
            if (prop.PropertyType == typeof(bool?))
            {
                if (string.IsNullOrEmpty(value))
                    return new bool?();
                return new bool?(bool.Parse(value));
            }
            if (prop.PropertyType == typeof(byte))
                return byte.Parse(value);
            if (prop.PropertyType == typeof(byte?))
            {
                if (string.IsNullOrEmpty(value))
                    return new byte?();
                return new byte?(byte.Parse(value));
            }
            if (prop.PropertyType == typeof(byte[]))
            {
                if (string.IsNullOrEmpty(value))
                    return new byte[0];
                return System.Text.Encoding.UTF8.GetBytes(value);
            }

            if (prop.PropertyType == typeof(char))
                return char.Parse(value);
            if (prop.PropertyType == typeof(char?))
            {
                if (string.IsNullOrEmpty(value))
                    return new char?();
                return new char?(char.Parse(value));
            }
#if NET6_0_OR_GREATER
            if (prop.PropertyType == typeof(DateOnly))
                return DateOnly.Parse(value);
            if (prop.PropertyType == typeof(DateOnly?))
            {
                if (string.IsNullOrEmpty(value))
                    return new DateOnly?();
                return new DateOnly?(DateOnly.Parse(value));
            }
#endif
            if (prop.PropertyType == typeof(DateTime))
                return DateTime.Parse(value, null, System.Globalization.DateTimeStyles.RoundtripKind);
            if (prop.PropertyType == typeof(DateTime?))
            {
                if (string.IsNullOrEmpty(value))
                    return new DateTime?();
                return new DateTime?(DateTime.Parse(value, null, System.Globalization.DateTimeStyles.RoundtripKind));
            }
            if (prop.PropertyType == typeof(DateTimeOffset))
                return DateTimeOffset.Parse(value);
            if (prop.PropertyType == typeof(DateTimeOffset?))
            {
                if (string.IsNullOrEmpty(value))
                    return new DateTimeOffset?();
                return new DateTimeOffset?(DateTimeOffset.Parse(value));
            }
            if (prop.PropertyType == typeof(decimal))
                return decimal.Parse(value);
            if (prop.PropertyType == typeof(decimal?))
            {
                if (string.IsNullOrEmpty(value))
                    return new decimal?();
                return new decimal?(decimal.Parse(value));
            }
            if (prop.PropertyType == typeof(double))
                return double.Parse(value);
            if (prop.PropertyType == typeof(double?))
            {
                if (string.IsNullOrEmpty(value))
                    return new double?();
                return new double?(double.Parse(value));
            }
            if (prop.PropertyType == typeof(Guid))
            {
                return Guid.Parse(value);
            }
            if (prop.PropertyType == typeof(Guid?))
            {
                if (string.IsNullOrEmpty(value))
                    return new Guid?();
                return new Guid?(Guid.Parse(value));
            }
            if (prop.PropertyType == typeof(short))
                return short.Parse(value);
            if (prop.PropertyType == typeof(short?))
            {
                if (string.IsNullOrEmpty(value))
                    return new short?();
                return new short?(short.Parse(value));
            }
            if (prop.PropertyType == typeof(int))
                return int.Parse(value);
            if (prop.PropertyType == typeof(int?))
            {
                if (string.IsNullOrEmpty(value))
                    return new int?();
                return new int?(int.Parse(value));
            }
            if (prop.PropertyType == typeof(long))
                return long.Parse(value);
            if (prop.PropertyType == typeof(long?))
            {
                if (string.IsNullOrEmpty(value))
                    return new long?();
                return new long?(long.Parse(value));
            }
            if (prop.PropertyType == typeof(sbyte))
                return sbyte.Parse(value);
            if (prop.PropertyType == typeof(sbyte?))
            {
                if (string.IsNullOrEmpty(value))
                    return new sbyte?();
                return new sbyte?(sbyte.Parse(value));
            }
            if (prop.PropertyType == typeof(Single))
                return Single.Parse(value);
            if (prop.PropertyType == typeof(Single?))
            {
                if (string.IsNullOrEmpty(value))
                    return new Single?();
                return new Single?(Single.Parse(value));
            }
            if (prop.PropertyType == typeof(string))
                return value;
#if NET6_0_OR_GREATER
            if (prop.PropertyType == typeof(TimeOnly))
                return TimeOnly.Parse(value);
            if (prop.PropertyType == typeof(TimeOnly?))
            {
                if (string.IsNullOrEmpty(value))
                    return new TimeOnly?();
                return new TimeOnly?(TimeOnly.Parse(value));
            }
#endif
            if (prop.PropertyType == typeof(TimeSpan))
                return TimeSpan.Parse(value);
            if (prop.PropertyType == typeof(TimeSpan?))
            {
                if (string.IsNullOrEmpty(value))
                    return new TimeSpan?();
                return new TimeSpan?(TimeSpan.Parse(value));
            }
            if (prop.PropertyType == typeof(UInt16))
                return UInt16.Parse(value);
            if (prop.PropertyType == typeof(UInt16?))
            {
                if (string.IsNullOrEmpty(value))
                    return new UInt16?();
                return new UInt16?(UInt16.Parse(value));
            }
            if (prop.PropertyType == typeof(UInt32))
                return UInt32.Parse(value);
            if (prop.PropertyType == typeof(UInt32?))
            {
                if (string.IsNullOrEmpty(value))
                    return new UInt32?();
                return new UInt32?(UInt32.Parse(value));
            }
            if (prop.PropertyType == typeof(UInt64))
                return UInt64.Parse(value);
            if (prop.PropertyType == typeof(UInt64?))
            {
                if (string.IsNullOrEmpty(value))
                    return new UInt64?();
                return new UInt64?(UInt64.Parse(value));
            }
#if NET7_0_OR_GREATER
            if (prop.PropertyType == typeof(UInt128))
                return UInt128.Parse(value);
            if (prop.PropertyType == typeof(UInt128?))
            {
                if (string.IsNullOrEmpty(value))
                    return new UInt128?();
                return new UInt128?(UInt128.Parse(value));
            }
#endif
            return value;
        }

        /// <summary>
        /// Determine if the ServiceQuery contains an aggregate filter.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool IsAggregate(this IServiceQuery query)
        {
            if (query.Filters == null)
                return false;
            return query.Filters.Where(x =>
            x.FilterType == ServiceQueryFilterType.Aggregate).Any();
        }

        /// <summary>
        /// Execute the Service Query and return a response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceQuery"></param>
        /// <param name="queryable"></param>
        /// <param name="serviceQueryOptions"></param>
        /// <returns></returns>
        public static ServiceQueryResponse<T> Execute<T>(this IServiceQuery serviceQuery, IQueryable<T> queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
            try
            {
                if (queryable == null)
                    return null;

                if (serviceQuery.IsAggregate())
                {
                    var aggregate = ExecuteAggregate<T>(serviceQuery, queryable, serviceQueryOptions);
                    int? count = new Nullable<int>();
                    if (serviceQuery.IncludeCount)
                    {
                        var countquery = serviceQuery.Apply(queryable, serviceQueryOptions);
                        count = countquery.Count();
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
                        Count = serviceQuery.IncludeCount ? query.Count() : 0,
                        List = query.Skip(skip).Take(serviceQuery.PageSize).ToList()
                    };
                }
                else
                {
                    return new ServiceQueryResponse<T>()
                    {
                        Count = serviceQuery.IncludeCount ? query.Count() : 0
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
        public static double? ExecuteAggregate<T>(this IServiceQuery serviceQuery, IQueryable<T> queryable, ServiceQueryOptions serviceQueryOptions = null)
        {
            try
            {
                if (serviceQuery == null || serviceQuery.Filters == null)
                    return null;

                var filter = serviceQuery.Filters.Where(x =>
                    x.FilterType == ServiceQueryFilterType.Aggregate).FirstOrDefault();
                if (filter == null)
                    return null;

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
                    return query.Count();

                var param = Expression.Parameter(entityType, "x");
                PropertyInfo prop = null;
                if (filter.Properties == null || filter.Properties.Count == 0)
                    throw new ServiceQueryException("requires at least 1 property");
                prop = GetProperty(properties, options, filter.Properties[0]);

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
                            return Convert.ToDouble(query.Average(decimalSelector));

                        case ServiceQueryAggregateType.Maximum:
                            return Convert.ToDouble(query.Max(decimalSelector));

                        case ServiceQueryAggregateType.Minimum:
                            return Convert.ToDouble(query.Min(decimalSelector));

                        case ServiceQueryAggregateType.Sum:
                            return Convert.ToDouble(query.Sum(decimalSelector));
                    }
                }
                if (prop.PropertyType == typeof(decimal?))
                {
                    var decimalNSelector = Expression.Lambda<Func<T, decimal?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            var aval = query.Average(decimalNSelector);
                            if (aval.HasValue)
                                return Convert.ToDouble(aval.Value);
                            return null;

                        case ServiceQueryAggregateType.Maximum:
                            var maval = query.Max(decimalNSelector);
                            if (maval.HasValue)
                                return Convert.ToDouble(maval.Value);
                            return null;

                        case ServiceQueryAggregateType.Minimum:
                            var mival = query.Min(decimalNSelector);
                            if (mival.HasValue)
                                return Convert.ToDouble(mival.Value);
                            return null;

                        case ServiceQueryAggregateType.Sum:
                            var sval = query.Sum(decimalNSelector);
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
                            return query.Average(doubleSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return query.Max(doubleSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(doubleSelector);

                        case ServiceQueryAggregateType.Sum:
                            return query.Sum(doubleSelector);
                    }
                }
                if (prop.PropertyType == typeof(double?))
                {
                    var doubleNSelector = Expression.Lambda<Func<T, double?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return query.Average(doubleNSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return query.Max(doubleNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(doubleNSelector);

                        case ServiceQueryAggregateType.Sum:
                            return query.Sum(doubleNSelector);
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
                            return query.Max(shortSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(shortSelector);

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
                            return query.Max(shortNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(shortNSelector);

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
                            return query.Average(intSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return query.Max(intSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(intSelector);

                        case ServiceQueryAggregateType.Sum:
                            return query.Sum(intSelector);
                    }
                }
                if (prop.PropertyType == typeof(int?))
                {
                    var intNSelector = Expression.Lambda<Func<T, int?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return query.Average(intNSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return query.Max(intNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(intNSelector);

                        case ServiceQueryAggregateType.Sum:
                            return query.Sum(intNSelector);
                    }
                }
                if (prop.PropertyType == typeof(long))
                {
                    var longSelector = Expression.Lambda<Func<T, long>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return query.Average(longSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return query.Max(longSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(longSelector);

                        case ServiceQueryAggregateType.Sum:
                            return query.Sum(longSelector);
                    }
                }
                if (prop.PropertyType == typeof(long?))
                {
                    var longNSelector = Expression.Lambda<Func<T, long?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return query.Average(longNSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return query.Max(longNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(longNSelector);

                        case ServiceQueryAggregateType.Sum:
                            return query.Sum(longNSelector);
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
                            return query.Max(sbyteSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(sbyteSelector);

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
                            return query.Max(sbyteNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(sbyteNSelector);

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
                            return query.Average(singleSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return query.Max(singleSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(singleSelector);

                        case ServiceQueryAggregateType.Sum:
                            return query.Sum(singleSelector);
                    }
                }
                if (prop.PropertyType == typeof(Single?))
                {
                    var singleNSelector = Expression.Lambda<Func<T, Single?>>(Expression.Property(param, prop), param);
                    switch (filter.AggregateType)
                    {
                        case ServiceQueryAggregateType.Average:
                            return query.Average(singleNSelector);

                        case ServiceQueryAggregateType.Maximum:
                            return query.Max(singleNSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(singleNSelector);

                        case ServiceQueryAggregateType.Sum:
                            return query.Sum(singleNSelector);
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
                            return query.Max(uint16Selector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(uint16Selector);

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
                            return query.Max(uint16NSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(uint16NSelector);

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
                            return query.Max(uint32Selector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(uint32Selector);

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
                            return query.Max(uint32NSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(uint32NSelector);

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
                            return query.Max(uint64Selector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(uint64Selector);

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
                            return query.Max(uint64NSelector);

                        case ServiceQueryAggregateType.Minimum:
                            return query.Min(uint64NSelector);

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
                            return (double)query.Max(uint128Selector);

                        case ServiceQueryAggregateType.Minimum:
                            return (double)query.Min(uint128Selector);

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
                            var val = query.Max(uint128NSelector);
                            if (val.HasValue)
                                return (double)val.Value;
                            return null;

                        case ServiceQueryAggregateType.Minimum:
                            var val2 = query.Min(uint128NSelector);
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
                throw new ServiceQueryException(ex.Message + "-ExecuteAggregate", ex);
            }
        }
    }
}