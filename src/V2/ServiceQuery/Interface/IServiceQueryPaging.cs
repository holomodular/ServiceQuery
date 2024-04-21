namespace ServiceQuery
{
    /// <summary>
    /// This interface provides paging.
    /// </summary>
    public partial interface IServiceQueryPaging
    {
        /// <summary>
        /// Determine if a Count() of the records should also be performed.
        /// </summary>
        bool IncludeCount { get; set; }

        /// <summary>
        /// The start page.
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        int PageSize { get; set; }
    }
}