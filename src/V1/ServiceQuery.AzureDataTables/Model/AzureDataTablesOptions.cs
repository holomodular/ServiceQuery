namespace ServiceQuery
{
    /// <summary>
    /// Options for AzureDataTables
    /// </summary>
    public class AzureDataTablesOptions
    {
        /// <summary>
        /// Determine whether to download all records if a count is requested.
        /// </summary>
        public bool DownloadAllRecordsForCount { get; set; }

        /// <summary>
        /// Determine whether to download all records if an aggregate operation is requested.
        /// </summary>
        public bool DownloadAllRecordsForAggregate { get; set; }

        /// <summary>
        /// Determine whether to download all records if a sort operation is requested.
        /// </summary>
        public bool DownloadAllRecordsForSort { get; set; }

        /// <summary>
        /// Determine whether to download all records if a string comparison operation is requested.
        /// </summary>
        public bool DownloadAllRecordsForStringComparison { get; set; }

        /// <summary>
        /// Determine whether to download all records if a distinct operation is requested.
        /// </summary>
        public bool DownloadAllRecordsForDistinct { get; set; }
    }
}