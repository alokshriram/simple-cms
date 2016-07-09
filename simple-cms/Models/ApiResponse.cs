namespace simple_cms.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// A generic API response object where you can put in a status message and the actual response type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// The message that conveys information about the payload
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The actual response payload
        /// </summary>
        public T Payload { get; set; }
    }
}