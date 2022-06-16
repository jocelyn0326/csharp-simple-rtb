namespace exchange_server.Models.BidModel
{
    public class BidderResponse
    {
        /// <summary>
        /// Bidder name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// >=0
        /// Bid price for this bid request. (Precision: Two digits after the decimal point.)
        /// </summary>
        public decimal price { get; set; }
    }
}