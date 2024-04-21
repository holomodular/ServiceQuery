using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// Internal object exposing properties used for a Service Query.
    /// </summary>
    public partial class ServiceQuery : IServiceQuery
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceQuery()
        {
            Filters = new List<ServiceQueryFilter>();
        }

        /// <summary>
        /// Collection of filters.
        /// </summary>
        public virtual List<ServiceQueryFilter> Filters { get; set; }

        /// <summary>
        /// The start page.
        /// </summary>
        public virtual int PageNumber { get; set; }

        /// <summary>
        /// The number of records per page.
        /// </summary>
        public virtual int PageSize { get; set; }

        /// <summary>
        /// Include the total Count() of records.
        /// </summary>
        public virtual bool IncludeCount { get; set; }
    }
}