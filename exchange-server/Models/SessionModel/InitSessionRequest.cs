using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static exchange_server.Startup;

namespace exchange_server.Models.SessionModel
{
    public class InitSessionRequest
    {
        private readonly string ENDPOINT = "https://localhost:44340/";
        //private readonly string ENDPOINT = "http://bidder-server/";


        /// <summary>
        /// Unique Id of the session
        /// </summary>
        [Required]
        public string session_id { get; set; }
        /// <summary>
        /// between 0 and 1000000
        /// The approximately number of bid requests in this session
        /// </summary>
        [Required]
        [Range(0, 1000000)]
        public int? estimated_traffic { get; set; }

        /// <summary>
        /// Array of object
        /// </summary>
        [Required]
        public List<Bidder> bidders { get;  set; }

        [Required]
        public BidderSetting bidder_setting { get; set; }

    }
}
