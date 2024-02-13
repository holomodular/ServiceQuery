using System.Collections.Generic;

namespace ServiceQuery
{
    /// <summary>
    /// This object contains the response properties from executing a SericeQueryRequest.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceQueryResponse<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceQueryResponse()
        {
            List = new List<T>();
        }

        /// <summary>
        /// The list of records.
        /// </summary>
        public List<T> List { get; set; }

        /// <summary>
        /// The Count() of records if IncludeCount() was added.
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// The aggregate value if an aggregate filter was included with the Service Query.
        /// </summary>
        public double? Aggregate { get; set; }
    }
}