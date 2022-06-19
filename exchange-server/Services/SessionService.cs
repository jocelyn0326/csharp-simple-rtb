using exchange_server.Models.BidModel;
using exchange_server.Models.SessionModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace exchange_server.Services
{
    public class SessionService
    {
        Dictionary<string, SessionData> SessionInfoDic;
        public SessionService()
        {
            if (SessionInfoDic == null) {
                SessionInfoDic = new Dictionary<string, SessionData>();
            } 
        }
        /// <summary>
        /// While post init_session
        /// </summary>
        /// <param name="session_id"></param>
        internal void AddSession(InitSessionRequest req)
        {
            SessionData sessionData = new SessionData(req);
            SessionInfoDic.Add(req.session_id, sessionData);
        }
        /// <summary>
        /// While post end_session
        /// </summary>
        internal bool EndSession(string session_id)
        {
            bool result = false;
            if (SessionInfoDic.ContainsKey(session_id))
            {
                SessionInfoDic.Remove(session_id);
                result = true;
            }
            return result;
        }

        internal void AddBidRequest(BidRequest req)
        {

            SessionInfoDic[req.session_id].BidRequestDic.Add(req.request_id, req);

        }

        internal string CheckBidRequestValidation(BidRequest req)
        {
            if (!SessionInfoDic.ContainsKey(req.session_id))
            {
                return "Please check if your session_id exists.";

            }
            if (!CheckRequestIdUniqueness(req.session_id, req.request_id))
            {
                return "request_id must be unique.";
            }
            return String.Empty;
        }

        internal List<Bidder> GestSessionBidders(string session_id)
        {
            return SessionInfoDic[session_id].sessionRequest.bidders.ToList();
        }

        private bool CheckRequestIdUniqueness(string session_id, string request_id)
        {

            if (SessionInfoDic[session_id].BidRequestDic.ContainsKey(request_id))
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// First Price auction
        /// Return null if highest bid is less than floor_price
        /// If there're same highest price bids, return the first bidder.
        /// </summary>
        /// <param name="bidders"></param>
        /// <returns></returns>
        internal WinBid PickAWinner(List<BidderResponse> bidders,decimal floor_price)
        {

            WinBid winbid = new WinBid() {
                price = 0
            };
            foreach(var bidder in bidders)
            {
                if(bidder.price > winbid.price)
                {
                    winbid.name = bidder.name;
                    winbid.price = bidder.price;
                }
            }
            if (winbid.price < floor_price)
            {
                return null;
            }
            return winbid;
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

        internal string GetBidderEndpoint(string session_id)
        {
            if (SessionInfoDic.ContainsKey(session_id))
            {
                var bidder = SessionInfoDic[session_id].sessionRequest.bidders.FirstOrDefault();
                if (null != bidder)
                {
                    return bidder.endpoint;
                }
                return String.Empty;
            }
            return String.Empty;
        }

        internal bool CheckSessionIdUniqueness(string session_id)
        {
            if (SessionInfoDic.ContainsKey(session_id)){
                return false;
            }
            return true;
        }

        internal Dictionary<string, SessionData> GetCurrentSessions() {
            return SessionInfoDic;
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
