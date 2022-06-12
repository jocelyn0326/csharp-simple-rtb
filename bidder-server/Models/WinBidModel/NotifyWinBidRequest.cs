using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.WinBidModel
{
    public class NotifyWinBidRequest
    {
        /// <summary>
        /// Unique Id of the session
        /// </summary>
        public string session_id { get; set; }
        /// <summary>
        /// Unique Id of the bid request
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        /// >=0
        /// The price which won this bid. (Precision: Two digits after the decimal point.)
        /// </summary>
        public decimal clear_price { get; set; }

    }
}
