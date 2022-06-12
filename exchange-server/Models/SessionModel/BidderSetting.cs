using System.ComponentModel.DataAnnotations;

namespace exchange_server.Models.SessionModel
{
    public class BidderSetting
    {
        /// <summary>
        /// Minimum bid for this bid request. (Precision: Two digits after the decimal point.)
        /// </summary>
        [Range(typeof(decimal), "0", "79228162514264337593543950335"),Required]
        public decimal? budget{ get; set; }
        /// <summary>
        /// between 0 and 1000000
        /// How many impressions that a bidder should win during this session
        /// </summary>
        [Range(0, 1000000), Required]
        public int? impression_goal { get; set; }
    }
}