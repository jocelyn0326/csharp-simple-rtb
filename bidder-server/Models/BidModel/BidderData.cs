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
        //key: bidder_name
        public Dictionary<string,BidderCurrentStatus> BiddersStatusDic { get; set; }

        /// <summary>
        /// Remaining estimated traffic in this session.
        /// When a bid received, minus one
        /// </summary>
        [Range(0, 1000000), Required]
        public int? session_remaining_estimated_traffic { get; set; }

        
    }

    public class BidderCurrentStatus
    {
        /// <summary>
        /// After wins a bid: remaining_budget = remaining_budget -  bid price.
        /// </summary>
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal remaining_budget { get; set; }

        /// <summary>
        /// After wins a bid: remaining_impression_goal = remaining_impression_goal -1.
        /// </summary>
        [Range(0, 1000000)]
        public int remaining_impression_goal { get; set; }
    }

}
