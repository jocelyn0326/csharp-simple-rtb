using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exchange_server.Models.BidModel
{
    /// <summary>
    /// Fire a bid request
    /// </summary>
    public class BidRequest
    {
        public BidRequest()
        {
            this.request_id = new Guid();
        }
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
        public Guid session_id {  get; set; }

        /// <summary>
        /// Unique Id of the user who fries this bid request
        /// </summary>
        public string user_id { get; set; }
        /// <summary>
        /// Unique Id of the bid request
        /// </summary>
        public Guid request_id
        {
            get;
            private set;
        }

    }
}
