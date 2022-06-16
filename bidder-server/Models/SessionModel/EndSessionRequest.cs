using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace bidder_server.Models.SessionModel
{
    public class EndSessionRequest
    {
        /// <summary>
        /// Unique Id of the session
        /// </summary>
        [Required]
        public string session_id { get; set; }

    }
}
