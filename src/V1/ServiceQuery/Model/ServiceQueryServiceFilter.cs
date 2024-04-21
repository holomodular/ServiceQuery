using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// This class contains the properties, values and enumeration used to define a Service Query filter.
    /// </summary>
    public class ServiceQueryServiceFilter
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceQueryServiceFilter()
        {
            Properties = new List<string>();
            Values = new List<string>();
        }

        /// <summary>
        /// The properties.
        /// </summary>
        public virtual List<string> Properties { get; set; }

        /// <summary>
        /// The values.
        /// </summary>
        public virtual List<string> Values { get; set; }

        /// <summary>
        /// The type of filter.
        /// </summary>
        public virtual ServiceQueryServiceFilterType FilterType { get; set; }
    }
}