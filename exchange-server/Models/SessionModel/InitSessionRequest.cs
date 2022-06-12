using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static exchange_server.Startup;

namespace exchange_server.Models.SessionModel
{
    public class InitSessionRequest
    {
        private readonly string endpoint = "https://localhost:44340/";

        public InitSessionRequest()
        {
            this.session_id = Guid.NewGuid().ToString();
            this.bidders = new List<Bidder>() {
                new Bidder{name="A",endpoint =  endpoint},
                new Bidder{name="B",endpoint =  endpoint},
                new Bidder{name="C",endpoint =  endpoint},
                new Bidder{name="D",endpoint =  endpoint},
                new Bidder{name="E",endpoint =  endpoint}

            };
           
        }
        /// <summary>
        /// Unique Id of the session
        /// </summary>
        public string session_id { get; private set; }
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
        public List<Bidder> bidders { get; private set; }

        public BidderSetting bidder_setting { get; set; }

    }
}
