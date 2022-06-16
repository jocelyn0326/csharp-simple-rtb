using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exchange_server.Models.BidModel
{
    /// <summary>
    /// Fire a bid request
    /// </summary>
    public class BidRequest
    {

        /// <summary>
        /// Minimum bid for this bid request. (Precision: Two digits after the decimal point.)
        /// </summary>
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price, Precision: Two digits after the decimal point.")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal floor_price { get; set; }
        /// <summary>
        /// Maximum time in milliseconds the exchange allows for bids to be received.
        /// </summary>

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "timeout_ms value should bigger than {1}")]
        public int? timeout_ms { get; set; }
        /// <summary>
        /// Unique Id of the session
        /// </summary>
        [Required]
        public string session_id {  get; set; }

        /// <summary>
        /// Unique Id of the user who fries this bid request
        /// </summary>
        [Required]
        public string user_id { get; set; }
        /// <summary>
        /// Unique Id of the bid request
        /// </summary>
        [Required]
        public string request_id
        {
            get;
            set;
        }

    }
}
