using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// This provides processing options for ServiceQuery.
    /// </summary>
    public class ServiceQueryOptions
    {
        /// <summary>
        /// Dictionary list of property name mappings.
        /// Exposed Class -> Internal Class
        /// Default will use all queryable class property names
        /// </summary>
        public Dictionary<string, string> PropertyNameMappings { get; set; }

        /// <summary>
        /// Determine whether property names must be case sensitive or throw an exception.
        /// Default is false.
        /// </summary>
        public bool PropertyNameCaseSensitive { get; set; }
    }
}