namespace ServiceQuery
{
    /// <summary>
    /// This interface defines the properties, values and types used for all Service Query operations.
    /// </summary>
    public partial interface IServiceQueryFilter
    {
        /// <summary>
        /// The properties.
        /// </summary>
        System.Collections.Generic.IList<string> Properties { get; set; }

        /// <summary>
        /// The values.
        /// </summary>
        System.Collections.Generic.IList<string> Values { get; set; }

        /// <summary>
        /// The type of aggregate.
        /// </summary>
        ServiceQueryAggregateType AggregateType { get; set; }

        /// <summary>
        /// The type of comparison.
        /// </summary>
        ServiceQueryCompareType CompareType { get; set; }

        /// <summary>
        /// The type of expression.
        /// </summary>
        ServiceQueryExpressionType ExpressionType { get; set; }

        /// <summary>
        /// The type of filter.
        /// </summary>
        ServiceQueryFilterType FilterType { get; set; }

        /// <summary>
        /// The type of include.
        /// </summary>
        ServiceQueryIncludeType IncludeType { get; set; }

        /// <summary>
        /// The type of sort.
        /// </summary>
        ServiceQuerySortType SortType { get; set; }
    }
}