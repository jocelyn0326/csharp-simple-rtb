using System;
using System.Net;

namespace bidder_server.Models.SessionModel
{
    public class SessionResponse
    {
        public HttpStatusCode result { get; set; }
        public string error { get; set; }
    }
}
