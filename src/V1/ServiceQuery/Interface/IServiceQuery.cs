using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// This is the internal ServiceQuery object
    /// </summary>
    public partial interface IServiceQuery : IServiceQueryPaging
    {
        /// <summary>
        /// The list of service query filters.
        /// </summary>
        List<ServiceQueryFilter> Filters { get; set; }
    }
}