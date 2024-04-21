using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// The builder used to create a ServiceQuery object.
    /// </summary>
    public partial class ServiceQueryBuilder : IServiceQueryBuilder
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceQueryBuilder()
        {
            Filters = new List<ServiceQueryFilter>();
            PageNumber = 1;
            PageSize = 1000;
        }

        /// <summary>
        /// Paging start page.
        /// </summary>
        public virtual int PageNumber { get; set; }

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public virtual int PageSize { get; set; }

        /// <summary>
        /// Include the Count() with the response.
        /// </summary>
        public virtual bool IncludeCount { get; set; }

        /// <summary>
        /// The list of filters.
        /// </summary>
        public virtual List<ServiceQueryFilter> Filters { get; set; }

        /// <summary>
        /// Create a new instance of a builder object, fluent-style.
        /// </summary>
        /// <returns></returns>
        public static IServiceQueryBuilder New()
        {
            return new ServiceQueryBuilder();
        }

        /// <summary>
        /// Get the storage query.
        /// </summary>
        /// <returns></returns>
        public virtual ServiceQuery Build()
        {
            return new ServiceQuery()
            {
                Filters = this.Filters,
                PageNumber = this.PageNumber,
                PageSize = this.PageSize,
                IncludeCount = this.IncludeCount,
            };
        }

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Expression(ServiceQueryExpressionType expressionType)
        {
            ServiceQueryFilter filter = new ServiceQueryFilter();
            filter.FilterType = ServiceQueryFilterType.Expression;
            filter.ExpressionType = expressionType;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder BeginExpression()
        {
            return Expression(ServiceQueryExpressionType.Begin);
        }

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Begin()
        {
            return Expression(ServiceQueryExpressionType.Begin);
        }

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder EndExpression()
        {
            return Expression(ServiceQueryExpressionType.End);
        }

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder End()
        {
            return Expression(ServiceQueryExpressionType.End);
        }

        /// <summary>
        /// Inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder And()
        {
            return Expression(ServiceQueryExpressionType.And);
        }

        /// <summary>
        /// Seperate inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Or()
        {
            return Expression(ServiceQueryExpressionType.Or);
        }

        /// <summary>
        /// Add properties to be selected.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Select(params string[] propertyNames)
        {
            ServiceQueryFilter filter = new ServiceQueryFilter();
            filter.FilterType = ServiceQueryFilterType.Select;
            foreach (string prop in propertyNames)
                filter.Properties.Add(prop);
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add distinct modifier.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Distinct()
        {
            ServiceQueryFilter filter = new ServiceQueryFilter();
            filter.FilterType = ServiceQueryFilterType.Distinct;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Between(string propertyName, string lowValue, string highValue)
        {
            //return IsGreaterThanOrEqual(propertyName, lowValue).And().IsLessThanOrEqual(propertyName, highValue);
            Filters.Add(new ServiceQueryFilter()
            {
                FilterType = ServiceQueryFilterType.Between,
                Properties = new List<string>() { propertyName },
                Values = new List<string>() { lowValue, highValue }
            });
            return this;
        }

        /// <summary>
        /// A string comparison expression that contains the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Contains(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.Contains, value);
        }

        /// <summary>
        /// A string comparison expression that starts with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder StartsWith(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.StartsWith, value);
        }

        /// <summary>
        /// A string comparison expression that ends with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder EndsWith(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.EndsWith, value);
        }

        /// <summary>
        /// An equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsEqual(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.Equal, value);
        }

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsNotEqual(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.NotEqual, value);
        }

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsGreaterThan(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.GreaterThan, value);
        }

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsGreaterThanOrEqual(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.GreaterThanEqual, value);
        }

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsLessThan(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.LessThan, value);
        }

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsLessThanOrEqual(string propertyName, string value)
        {
            return Compare(propertyName, ServiceQueryCompareType.LessThanEqual, value);
        }

        /// <summary>
        /// Add a comparison expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="comparisonType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Compare(string propertyName, ServiceQueryCompareType comparisonType, string value)
        {
            ServiceQueryFilter filter = new ServiceQueryFilter();
            filter.FilterType = ServiceQueryFilterType.Compare;
            filter.Properties.Add(propertyName);
            filter.CompareType = comparisonType;
            filter.Values.Add(value);
            Filters.Add(filter);
            return this;
        }

        ///// <summary>
        ///// Add a property comparison expression.
        ///// </summary>
        ///// <param name="propertyNameLeft"></param>
        ///// <param name="comparisonType"></param>
        ///// <param name="propertyNameRight"></param>
        ///// <returns></returns>
        //public virtual IServiceQueryBuilder PropertyCompare(string propertyNameLeft, ServiceQueryCompareType comparisonType, string propertyNameRight)
        //{
        //    ServiceQueryFilter filter = new ServiceQueryFilter();
        //    filter.FilterType = ServiceQueryFilterType.PropertyCompare;
        //    filter.Properties.Add(propertyNameLeft);
        //    filter.Properties.Add(propertyNameRight);
        //    filter.CompareType = comparisonType;
        //    Filters.Add(filter);
        //    return this;
        //}

        /// <summary>
        /// Add a is null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsNull(string propertyName)
        {
            return Null(propertyName, true);
        }

        /// <summary>
        /// Add a null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Null(string propertyName, bool isNull)
        {
            ServiceQueryFilter filter = new ServiceQueryFilter();
            filter.FilterType = ServiceQueryFilterType.Null;
            filter.Properties.Add(propertyName);
            if (isNull)
                filter.IncludeType = ServiceQueryIncludeType.Include;
            else
                filter.IncludeType = ServiceQueryIncludeType.NotInclude;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a is not null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsNotNull(string propertyName)
        {
            return Null(propertyName, false);
        }

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsInSet(string propertyName, params string[] values)
        {
            return Set(propertyName, true, values);
        }

        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Set(string propertyName, bool inSet, params string[] values)
        {
            ServiceQueryFilter filter = new ServiceQueryFilter();
            filter.FilterType = ServiceQueryFilterType.Set;
            filter.Properties.Add(propertyName);
            if (inSet)
                filter.IncludeType = ServiceQueryIncludeType.Include;
            else
                filter.IncludeType = ServiceQueryIncludeType.NotInclude;
            foreach (string value in values)
                filter.Values.Add(value);
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder IsNotInSet(string propertyName, params string[] values)
        {
            return Set(propertyName, false, values);
        }

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Sort(string propertyName, bool sortAscending)
        {
            ServiceQueryFilter filter = new ServiceQueryFilter();
            filter.FilterType = ServiceQueryFilterType.Sort;
            filter.Properties.Add(propertyName);
            if (sortAscending)
                filter.SortType = ServiceQuerySortType.Ascending;
            else
                filter.SortType = ServiceQuerySortType.Descending;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a sort expression ascending.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder SortAsc(string propertyName)
        {
            return Sort(propertyName, true);
        }

        /// <summary>
        /// Add a sort expression descending.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder SortDesc(string propertyName)
        {
            return Sort(propertyName, false);
        }

        /// <summary>
        /// Add an averate aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Average(string propertyName)
        {
            return Aggregate(propertyName, ServiceQueryAggregateType.Average);
        }

        ///// <summary>
        ///// Add a binary checksum aggregate expression.
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        ////public virtual IServiceQueryBuilder BinaryChecksum(string propertyName)
        ////{
        ////    return Aggregate(propertyName, ServiceQueryAggregateType.BinaryChecksum);
        ////}

        /// <summary>
        /// Add a count aggregate expression.
        /// </summary>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Count()
        {
            return Aggregate(string.Empty, ServiceQueryAggregateType.Count);
        }

        ///// <summary>
        ///// Add a checksum aggregate expression.
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        ////public virtual IServiceQueryBuilder Checksum(string propertyName)
        ////{
        ////    return Aggregate(propertyName, ServiceQueryAggregateType.Checksum);
        ////}

        /// <summary>
        /// Add a maximum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Maximum(string propertyName)
        {
            return Aggregate(propertyName, ServiceQueryAggregateType.Maximum);
        }

        /// <summary>
        /// Add a minumum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Minimum(string propertyName)
        {
            return Aggregate(propertyName, ServiceQueryAggregateType.Minimum);
        }

        /// <summary>
        /// Add a sum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Sum(string propertyName)
        {
            return Aggregate(propertyName, ServiceQueryAggregateType.Sum);
        }

        /// <summary>
        /// Add an aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="aggregateType"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Aggregate(string propertyName, ServiceQueryAggregateType aggregateType)
        {
            ServiceQueryFilter filter = new ServiceQueryFilter();
            filter.FilterType = ServiceQueryFilterType.Aggregate;
            filter.Properties.Add(propertyName);
            filter.AggregateType = aggregateType;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Setup paging.
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="numberPerPage"></param>
        /// <param name="includeCount"></param>
        /// <returns></returns>
        public virtual IServiceQueryBuilder Paging(int startPage, int numberPerPage, bool includeCount)
        {
            PageNumber = startPage;
            PageSize = numberPerPage;
            this.IncludeCount = includeCount;
            return this;
        }
    }
}