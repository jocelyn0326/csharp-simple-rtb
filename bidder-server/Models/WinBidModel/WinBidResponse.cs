using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.WinBidModel
{
    public class WinBidResponse
    {
        public Status result { get; set; }
        public string error { get; set; }
    }
}
