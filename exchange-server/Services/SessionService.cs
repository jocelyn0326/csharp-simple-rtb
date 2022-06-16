using exchange_server.Models.BidModel;
using exchange_server.Models.SessionModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace exchange_server.Services
{
    public class SessionService
    {
        Dictionary<string, SessionData> SessionStatusDic;
        public SessionService()
        {
            if (SessionStatusDic == null) {
                SessionStatusDic = new Dictionary<string, SessionData>();
            } 
        }
        /// <summary>
        /// While post init_session
        /// </summary>
        /// <param name="session_id"></param>
        internal void AddSession(InitSessionRequest req)
        {
            SessionData sessionData = new SessionData(req);
            SessionStatusDic.Add(req.session_id, sessionData);
        }
        /// <summary>
        /// While post end_session
        /// </summary>
        internal bool EndSession(string session_id)
        {
            bool result = false;
            if (SessionStatusDic.ContainsKey(session_id))
            {
                SessionStatusDic.Remove(session_id);
                result = true;
            }
            return result;
        }

        internal void AddBidRequest(BidRequest req)
        {

            SessionStatusDic[req.session_id].BidRequestDic.Add(req.request_id, req);

        }

        internal string CheckBidRequestValidation(BidRequest req)
        {
            if (!SessionStatusDic.ContainsKey(req.session_id))
            {
                return "Please check if your session_id exists.";

            }
            if (!CheckRequestIdUniqueness(req.session_id, req.request_id))
            {
                return "request_id must be unique.";
            }
            return String.Empty;
        }

        private bool CheckRequestIdUniqueness(string session_id, string request_id)
        {

            if (SessionStatusDic[session_id].BidRequestDic.ContainsKey(request_id))
            {
                return false;
            }
            return true;
        }

        internal bool CheckBidderNameUniqueness(List<Bidder> bidders)
        {
            List<String> duplicates = bidders.GroupBy(x => x.name)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key)
                             .ToList();
            if(duplicates.Count >0)
            {
                return false;
            }
            return true;
        }

        internal bool CheckSessionIdUniqueness(string session_id)
        {
            if (SessionStatusDic.ContainsKey(session_id)){
                return false;
            }
            return true;
        }

        internal Dictionary<string, SessionData> GetCurrentSessions() {
            return SessionStatusDic;
        }

    }

    public class SessionData
    {
        public SessionData(InitSessionRequest request)
        {
            this.sessionRequest = request;
            if (BidRequestDic == null)
            {
                BidRequestDic = new Dictionary<string, BidRequest>();
            }
        }
        public InitSessionRequest sessionRequest { get; set; }

        public Dictionary<string, BidRequest> BidRequestDic { get; set; }
    }
}
