using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exchange_server.Models.SessionModel
{
    public class EndSessionRequest
    {
        public EndSessionRequest()
        {
            //this.endpoint = "https://localhost:44340/";
            this.endpoint = "http://bidder-server/";
        }
        public string session_id { get; set; }
        public string endpoint { get; private set; }
    }
}
