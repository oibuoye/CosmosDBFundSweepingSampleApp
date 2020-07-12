using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CosmosDBFundSweepingSampleApp.Core.HelperModels
{
    public class APIResponse
    {
        /// <summary>
        /// Does the response have any errors.
        /// <para>If the response has any error the bool value is true.</para>
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// This contains the response object.
        /// <para>Return object is the result of the request, if error is true, returned value is a list of error model <see cref="ErrorModel"/></para>
        /// </summary>
        public dynamic ResponseObject { get; set; }
    }
}
