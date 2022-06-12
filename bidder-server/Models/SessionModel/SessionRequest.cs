using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.SessionModel
{
    public class SessionRequest
    {

        /// <summary>
        /// Unique Id of the session
        /// </summary>
        public string session_id { get; set; }
        /// <summary>
        /// between 0 and 1000000
        /// The approximately number of bid requests in this session
        /// </summary>
        [Range(0, 1000000),Required]
        public int? estimated_traffic { get; set; }

        public BidderSetting bidder_setting { get; set; }
       

    }
}
