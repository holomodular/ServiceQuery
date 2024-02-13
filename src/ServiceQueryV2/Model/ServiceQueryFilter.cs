using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// The object contains the operation used in a Service Query.
    /// </summary>
    public partial class ServiceQueryFilter : IServiceQueryFilter
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceQueryFilter()
        {
            Properties = new List<string>();
            Values = new List<string>();
        }

        /// <summary>
        /// The columns.
        /// </summary>
        public virtual System.Collections.Generic.IList<string> Properties { get; set; }

        /// <summary>
        /// The values.
        /// </summary>
        public virtual System.Collections.Generic.IList<string> Values { get; set; }

        /// <summary>
        /// The type of aggregate.
        /// </summary>
        public virtual ServiceQueryAggregateType AggregateType { get; set; }

        /// <summary>
        /// The type of comparison.
        /// </summary>
        public virtual ServiceQueryCompareType CompareType { get; set; }

        /// <summary>
        /// The type of expression.
        /// </summary>
        public virtual ServiceQueryExpressionType ExpressionType { get; set; }

        /// <summary>
        /// The type of filter.
        /// </summary>
        public virtual ServiceQueryFilterType FilterType { get; set; }

        /// <summary>
        /// The type of include.
        /// </summary>
        public virtual ServiceQueryIncludeType IncludeType { get; set; }

        /// <summary>
        /// The type of sort.
        /// </summary>
        public virtual ServiceQuerySortType SortType { get; set; }
    }
}