using System.ComponentModel.DataAnnotations;

namespace exchange_server.Models.BidModel
{
    public class BidderResponse
    {
        /// <summary>
        /// Bidder name
        /// </summary>
        [Required]
        public string name { get; set; }
        /// <summary>
        /// >=0
        /// Bid price for this bid request. (Precision: Two digits after the decimal point.)
        /// </summary>
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price, Precision: Two digits after the decimal point.")]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]

        public decimal price { get; set; }
    }
}