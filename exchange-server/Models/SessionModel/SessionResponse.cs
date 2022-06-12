
using System.Net;

namespace exchange_server.Models.SessionModel
{
    public class SessionResponse
    {
        public HttpStatusCode result { get; set; }
        public string error { get; set; }

    }
}
