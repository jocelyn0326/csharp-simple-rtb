using bidder_server.Models.SessionModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.BidModel
{
    public class BidderData
    {
        public Dictionary<string,BidderCurrentStatus> BiddersStatusDic { get; set; }

        /// <summary>
        /// Remaining estimated traffic in this session.
        /// When a bid received, minus one
        /// </summary>
        [Range(0, 1000000), Required]
        public int? session_remaining_estimated_traffic { get; set; }

        public BidderData(SessionRequest request)
        {
            this.session_remaining_estimated_traffic = request.estimated_traffic;
            this.BiddersStatusDic = new Dictionary<string, BidderCurrentStatus>();
            BidderCurrentStatus bidderCurrentStatus = new BidderCurrentStatus() {
                remaining_budget = request.bidder_setting.budget,
                remaining_impression_goal = request.bidder_setting.impression_goal

            };
            foreach (var bidder in request.bidders)
            {
                BiddersStatusDic.Add(bidder.name, bidderCurrentStatus);
            }
        }

        
    }

    public class BidderCurrentStatus
    {
        /// <summary>
        /// After wins a bid: remaining_budget = remaining_budget -  bid price.
        /// </summary>
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? remaining_budget { get; set; }

        /// <summary>
        /// After wins a bid: remaining_impression_goal = remaining_impression_goal -1.
        /// </summary>
        [Range(0, 1000000)]
        public int? remaining_impression_goal { get; set; }
    }

}
