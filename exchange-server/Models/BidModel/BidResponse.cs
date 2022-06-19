using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exchange_server.Models.BidModel
{
    public class BidResponse
    {
        public BidResponse()
        {
            if(bidderResponses == null)
            {
                bidderResponses = new List<BidderResponse>();
            }
        }
        /// <summary>
        /// Unique Id of the session
        /// </summary>
        public string session_id { get; set; }
        /// <summary>
        /// Unique Id of the bid request
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        ///  Winner bid's info, including bidder name and price. Note: Return null if there is no winner.
        /// </summary>
        public WinBid win_bid { get; set; }

        public List<BidderResponse> bidderResponses { get; set; }

    }
}
