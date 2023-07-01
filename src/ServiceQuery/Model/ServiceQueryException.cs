using System;

namespace ServiceQuery
{
    /// <summary>
    /// The default exception thrown if any errors occur while processing a Service Query.
    /// </summary>
    public class ServiceQueryException : Exception
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="message"></param>
        public ServiceQueryException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public ServiceQueryException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}