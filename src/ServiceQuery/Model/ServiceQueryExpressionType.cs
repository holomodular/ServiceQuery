namespace ServiceQuery
{
    /// <summary>
    /// Enumeration of expression types.
    /// </summary>
    public enum ServiceQueryExpressionType : int
    {
        /// <summary>
        /// Inclusive Operator.
        /// </summary>
        And = 0,

        /// <summary>
        /// Exclusive Operator.
        /// </summary>
        Or = 1,

        /// <summary>
        /// Begin expression.
        /// </summary>
        Begin = 2,

        /// <summary>
        /// End expression.
        /// </summary>
        End = 3
    }
}