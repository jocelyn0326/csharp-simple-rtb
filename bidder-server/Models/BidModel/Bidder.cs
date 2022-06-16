using bidder_server.Models.SessionModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.BidModel
{
     
    public class Bidder
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string endpoint
        {
            get;
            set;
        }
        
    }
}
