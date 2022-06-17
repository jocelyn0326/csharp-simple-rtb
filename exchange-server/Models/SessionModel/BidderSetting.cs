using System.ComponentModel.DataAnnotations;

namespace exchange_server.Models.SessionModel
{
    public class BidderSetting
    {

        /// <summary>
        /// Minimum bid for this bid request. (Precision: Two digits after the decimal point.)
        /// </summary>
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price, Precision: Two digits after the decimal point.")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal? budget{ get; set; }
        /// <summary>
        /// between 0 and 1000000
        /// How many impressions that a bidder should win during this session
        /// </summary>
        [Range(0, 1000000), Required]
        public int? impression_goal { get; set; }
    }
}