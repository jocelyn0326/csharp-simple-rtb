using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.BidModel
{
    /// <summary>
    /// Opportunity to acquire impressions
    /// </summary>
    public class BidRequest
    {
        /// <summary>
        /// Minimum bid for this bid request. (Precision: Two digits after the decimal point.)
        /// </summary>
        public int floor_price { get; set; }
        /// <summary>
        /// Maximum time in milliseconds the exchange allows for bids to be received.
        /// </summary>
        public int timeout_ms { get; set; }
        /// <summary>
        /// Unique Id of the session
        /// </summary>
        public string session_id { get; set; }
        /// <summary>
        /// Unique Id of the user who fres this bid request
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// Unique Id of the bid request
        /// </summary>
        public string request_id { get; set; }


    }
}
