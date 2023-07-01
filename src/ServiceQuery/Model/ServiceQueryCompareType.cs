namespace ServiceQuery
{
    /// <summary>
    /// Enumeration of comparison types.
    /// </summary>
    public enum ServiceQueryCompareType : int
    {
        /// <summary>
        /// Equality.
        /// </summary>
        Equal = 0,

        /// <summary>
        /// InEquality.
        /// </summary>
        NotEqual = 1,

        /// <summary>
        /// Contains.
        /// </summary>
        Contains = 2,

        /// <summary>
        /// Begins with.
        /// </summary>
        StartsWith = 3,

        /// <summary>
        /// Ends with.
        /// </summary>
        EndsWith = 4,

        /// <summary>
        /// Less than.
        /// </summary>
        LessThan = 5,

        /// <summary>
        /// Less than or equal to.
        /// </summary>
        LessThanEqual = 6,

        /// <summary>
        /// Greater than.
        /// </summary>
        GreaterThan = 7,

        /// <summary>
        /// Greater than or equal to.
        /// </summary>
        GreaterThanEqual = 8,
    }
}