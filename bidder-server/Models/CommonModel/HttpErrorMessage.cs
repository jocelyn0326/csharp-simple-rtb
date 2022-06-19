using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.CommonModel
{
    public class HttpErrorMessage
    {
        /// <summary>
        /// Custom message, error code or objects to describe the error.
        /// </summary>
        public string error { get; set; }
    }
}
