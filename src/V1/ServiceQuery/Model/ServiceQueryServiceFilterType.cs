namespace ServiceQuery
{
    /// <summary>
    /// Enumeration of all operations exposed via the ServiceQueryFilter object.
    /// </summary>
    public enum ServiceQueryServiceFilterType : int
    {
        /// <summary>
        /// Comparison Equal.
        /// </summary>
        equal = 0,

        /// <summary>
        /// Comparison Not Equal.
        /// </summary>
        notequal = 1,

        /// <summary>
        /// Comparison Contains.
        /// </summary>
        contains = 2,

        /// <summary>
        /// Comparison Begins With.
        /// </summary>
        startswith = 3,

        /// <summary>
        /// Comparison Ends With.
        /// </summary>
        endswith = 4,

        /// <summary>
        /// Comparison Less Than.
        /// </summary>
        lessthan = 5,

        /// <summary>
        /// Comparison Less Than Or Equal To.
        /// </summary>
        lessthanorequal = 6,

        /// <summary>
        /// Comparison Greater Than.
        /// </summary>
        greaterthan = 7,

        /// <summary>
        /// Comparison Greater Than Or Equal To.
        /// </summary>
        greaterthanorequal = 8,

        /// <summary>
        /// Comparison Between.
        /// </summary>
        between = 9,

        /// <summary>
        /// Aggregate Count.
        /// </summary>
        count = 10,

        /// <summary>
        /// Aggregate Average.
        /// </summary>
        average = 11,

        /// <summary>
        /// Aggregate Minimum.
        /// </summary>
        minimum = 12,

        /// <summary>
        /// Aggregate Maximum.
        /// </summary>
        maximum = 13,

        /// <summary>
        /// Aggregate Sum.
        /// </summary>
        sum = 14,

        /// <summary>
        /// Distinct.
        /// </summary>
        distinct = 15,

        /// <summary>
        /// Expression And.
        /// </summary>
        and = 16,

        /// <summary>
        /// Expression Or.
        /// </summary>
        or = 17,

        /// <summary>
        /// Expression Begin.
        /// </summary>
        begin = 18,

        /// <summary>
        /// Expression End.
        /// </summary>
        end = 19,

        /// <summary>
        /// Set Include.
        /// </summary>
        inset = 20,

        /// <summary>
        /// Set Not include.
        /// </summary>
        notinset = 21,

        /// <summary>
        /// IsNull.
        /// </summary>
        isnull = 22,

        /// <summary>
        /// IsNotNull.
        /// </summary>
        isnotnull = 23,

        /// <summary>
        /// Select.
        /// </summary>
        select = 24,

        /// <summary>
        /// Sort Ascending.
        /// </summary>
        sortasc = 25,

        /// <summary>
        /// Sort Descending.
        /// </summary>
        sortdesc = 26,

        /// <summary>
        /// Page Number.
        /// </summary>
        pagenumber = 27,

        /// <summary>
        /// Page Size.
        /// </summary>
        pagesize = 28,

        /// <summary>
        /// Include Count.
        /// </summary>
        includecount = 29,

        ///// <summary>
        ///// Binary Checksum.
        ///// </summary>
        ////binarychecksum = 30,

        ///// <summary>
        ///// Checksum.
        ///// </summary>
        ////checksum = 31,
    }
}