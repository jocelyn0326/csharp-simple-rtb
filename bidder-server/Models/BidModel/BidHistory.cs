using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.BidModel
{
    public class BidHistory
    {
        public BidHistory()
        {
            if (BidderResponseDicForUser == null)
            {
                BidderResponseDicForUser= new Dictionary<string, BidderResponse>();
            }
        }
        //key: user_id, value: BidderResponses
        public Dictionary<string, BidderResponse> BidderResponseDicForUser { get; set; }
    }

    public class BidderResponse
    {
        public BidderResponse() {
            if (BidderResponsesDic == null)
            {
                BidderResponsesDic = new Dictionary<string, decimal>();
            }
        }
        
        //key: bidder_name, value: price
        public Dictionary<string, decimal> BidderResponsesDic { get; set; }
    }

}
