using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// This interface defines the filters used on a ServiceQueryRequest object.
    /// </summary>
    public partial interface IServiceQueryRequest
    {
        /// <summary>
        /// The list of service query filters.
        /// </summary>
        List<ServiceQueryServiceFilter> Filters { get; set; }
    }
}