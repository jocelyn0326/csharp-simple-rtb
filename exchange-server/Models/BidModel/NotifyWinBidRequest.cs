using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exchange_server.Models.BidModel
{
    public class NotifyWinBidRequest
    {
        /// <summary>
        /// Unique Id of the session
        /// </summary>
        [Required]
        public string session_id { get; set; }

        /// <summary>
        /// Unique Id of the bid request
        /// </summary>
        public string request_id { get; set; }

        /// <summary>
        /// >=0
        ///The price which won this bid. (Precision: Two digits after the decimal point.)
        /// </summary>
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price, Precision: Two digits after the decimal point.")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal clear_price { get; set; }

        public string name { get; set; }

    }
}
