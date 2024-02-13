using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// This is the exposed Service Query request object that should be used with APIs.
    /// </summary>
    public class ServiceQueryRequest : IServiceQueryRequest
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceQueryRequest()
        {
            Filters = new List<ServiceQueryServiceFilter>();
        }

        /// <summary>
        /// Collection of filters.
        /// </summary>
        public virtual List<ServiceQueryServiceFilter> Filters { get; set; }
    }
}