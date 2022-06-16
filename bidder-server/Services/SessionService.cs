using bidder_server.Models.BidModel;
using bidder_server.Models.SessionModel;
using System.Collections.Generic;

namespace bidder_server.Services
{
    public class SessionService
    {
        /// <summary>
        /// Collect current session_id's Bidder situation.
        /// </summary>
        Dictionary<string, BidderData> SessionBidderStatusDic;
        public SessionService()
        {
            if (SessionBidderStatusDic == null)
            {
                SessionBidderStatusDic = new Dictionary<string, BidderData>();
            }
        }
        /// <summary>
        /// Collect all the session & bidder's information.
        /// </summary>
        /// <param name="session_id"></param>
        public bool AddSession(string session_id, SessionRequest request)
        {
            // session_id is unique
            if (!SessionBidderStatusDic.ContainsKey(session_id)) {

                BidderData bidderData = new BidderData(request);
                SessionBidderStatusDic.Add(session_id, bidderData);
                return true;
            }
            return false;
        }
        /// <summary>
        /// While post end_session
        /// </summary>
        public bool EndSession(string session_id)
        {
            // If the key(session_id) is not exit in the SessionStatusDic, return false.
            if (!SessionBidderStatusDic.ContainsKey(session_id))
            {
                return false;
            }
            // Remove the key(session_id
            SessionBidderStatusDic.Remove(session_id);
            return true;
        }
    }
}
