using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models
{
    public enum Status
    {
        Success = 200,
        NotBid = 204,
        Error = 400
    }
}
