using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// This provides processing options for Service Query.
    /// </summary>
    public class ServiceQueryOptions
    {
        /// <summary>
        /// Dictionary list of property name mappings.
        /// Exposed Class -> Internal Class
        /// </summary>
        public Dictionary<string, string> PropertyNameMappings { get; set; }

        /// <summary>
        /// Determine whether property names must be case sensitive or throw an exception.
        /// </summary>
        public bool PropertyNameCaseSensitive { get; set; }
    }
}