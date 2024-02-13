using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// This object is used for pre-processing filters into query sets.
    /// </summary>
    public class ServiceQueryFilterSet
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceQueryFilterSet()
        {
            SelectFilters = new List<IServiceQueryFilter>();
            WhereFilters = new List<List<IServiceQueryFilter>>();
            SortFilters = new List<IServiceQueryFilter>();
        }

        /// <summary>
        /// The list of select filters.
        /// </summary>
        public List<IServiceQueryFilter> SelectFilters { get; set; }

        /// <summary>
        /// This list of where filters.
        /// </summary>
        public List<List<IServiceQueryFilter>> WhereFilters { get; set; }

        /// <summary>
        /// The list of sort filters.
        /// </summary>
        public List<IServiceQueryFilter> SortFilters { get; set; }
    }
}