namespace bidder_server.Models.BidModel
{
    public class BidResponse
    {
        /// <summary>
        /// Unique Id of the session
        /// </summary>
        public string session_id { get; set; }
        /// <summary>
        /// Unique Id of the bid request
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        /// Bid price for this bid request. Note: Return -1 for no bid situation. (Precision: Two digits after the decimal point)
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// bidder name
        /// </summary>
        public string name { get; set; }

        
    }
}
