namespace ServiceQuery
{
    /// <summary>
    /// Enumeration of filter types.
    /// </summary>
    public enum ServiceQueryFilterType : int
    {
        /// <summary>
        /// Comparison.
        /// </summary>
        Compare = 0,

        /// <summary>
        /// Aggregate.
        /// </summary>
        Aggregate = 1,

        /// <summary>
        /// Between.
        /// </summary>
        Between = 2,

        /// <summary>
        /// Distinct.
        /// </summary>
        Distinct = 3,

        /// <summary>
        /// Expression.
        /// </summary>
        Expression = 4,

        /// <summary>
        /// Null.
        /// </summary>
        Null = 5,

        /// <summary>
        /// PropertyComparison.
        /// </summary>
        PropertyCompare = 6,

        /// <summary>
        /// Select.
        /// </summary>
        Select = 7,

        /// <summary>
        /// Set.
        /// </summary>
        Set = 8,

        /// <summary>
        /// Sort.
        /// </summary>
        Sort = 9,
    }
}