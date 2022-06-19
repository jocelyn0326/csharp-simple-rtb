using bidder_server.Models.BidModel;
using bidder_server.Models.SessionModel;
using bidder_server.Models.WinBidModel;
using System;
using System.Collections.Generic;
using static bidder_server.Models.BidModel.BidHistory;

namespace bidder_server.Services
{

    public class SessionService
    {
        /// <summary>
        /// Collect current session_id's Bidder situation.
        /// key: session_id
        /// </summary>
        Dictionary<string, BidderData> SessionBidderStatusDic;
        /// <summary>
        /// Coller all bids history in the session
        /// key: session_id
        /// </summary>
        Dictionary<string, BidHistory> SessionBidHistory;
        public SessionService()
        {
            if (SessionBidderStatusDic == null)
            {
                SessionBidderStatusDic = new Dictionary<string, BidderData>();
            }
            if(SessionBidHistory == null)
            {
                SessionBidHistory = new Dictionary<string, BidHistory>();
            }
        }

        internal BidResponse GetBidPrice(BidRequest req)
        {
            BidResponse response = new BidResponse();
            response.session_id = req.session_id;
            response.request_id = req.request_id;

            var bidderStatus = SessionBidderStatusDic[req.session_id].BiddersStatusDic[req.name];

            #region Check if the user_id is posted, the bidded price for the bidder have enough remaining budget, and the imression goal has reached.
            if (SessionBidHistory.ContainsKey(req.session_id) && SessionBidHistory[req.session_id].BidderResponseDicForUser.ContainsKey(req.user_id) && SessionBidHistory[req.session_id].BidderResponseDicForUser[req.user_id].BidderResponsesDic.ContainsKey(req.name))
            {
                var bidderHistoryPrice = SessionBidHistory[req.session_id].BidderResponseDicForUser[req.user_id].BidderResponsesDic[req.name];
                if(bidderStatus.remaining_impression_goal == 0 || bidderHistoryPrice == -1)
                {
                    response.name = req.name;
                    response.price = -1;
                    return response;
                }
                if(bidderStatus.remaining_budget >= bidderHistoryPrice)
                {
                    response.name = req.name;
                    response.price = bidderHistoryPrice;
                    return response;

                }
            }
            #endregion

            #region If the bidder should bid

            double min = (double)req.floor_price;
            double max = (double)bidderStatus.remaining_budget;
            decimal price = req.floor_price;
            //To bid: 1.remaining_impression_goal > 0 2. floor_price>remaining_budget 3. If wins the bid, the bidder still has enought budget to get other impressions.
            if (bidderStatus.remaining_impression_goal > 0 && bidderStatus.remaining_budget >= req.floor_price   &&  ((bidderStatus.remaining_budget - req.floor_price) / (bidderStatus.remaining_impression_goal - 1)) >= (decimal)0.01 ) 
            {
                do
                {
                    Random rand = new Random();
                    double range = max - min;
                    double sample = rand.NextDouble();
                    double scaled = (sample * range) + min;
                    price = Decimal.Round((decimal)scaled,2);
                    max = (double)price;
                } while (((bidderStatus.remaining_budget - price) / (bidderStatus.remaining_impression_goal - 1)) < (decimal)0.01);
                response.name = req.name;
                response.price = price;
            }
            //Not to bid
            else
            {
                response.name = req.name;
                response.price = -1;
            }
            #endregion
            #region Add bid response into SessionBidHistory

            if (!SessionBidHistory.ContainsKey(req.session_id)){
                SessionBidHistory.Add(req.session_id, new BidHistory());
            }
            if (!SessionBidHistory[req.session_id].BidderResponseDicForUser.ContainsKey(req.user_id))
            {
                SessionBidHistory[req.session_id].BidderResponseDicForUser.Add(req.user_id, new BidderResponse());
            }
            if (!SessionBidHistory[req.session_id].BidderResponseDicForUser[req.user_id].BidderResponsesDic.ContainsKey(response.name))
            {
                SessionBidHistory[req.session_id].BidderResponseDicForUser[req.user_id].BidderResponsesDic.Add(response.name, response.price);
            }
            else
            {
                SessionBidHistory[req.session_id].BidderResponseDicForUser[req.user_id].BidderResponsesDic[response.name] = response.price;
            }


            #endregion
            return response;
        }

        internal bool WinNotify(NotifyWinBidRequest req)
        {
            var bidderStatus = SessionBidderStatusDic[req.session_id].BiddersStatusDic[req.name];
            bidderStatus.remaining_budget -= req.clear_price;
            bidderStatus.remaining_impression_goal -= 1;
            if (bidderStatus.remaining_budget <0)
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// Collect all the session & bidder's information.
        /// </summary>
        /// <param name="session_id"></param>
        internal bool AddSession(string session_id, SessionRequest request)
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
        internal bool EndSession(string session_id)
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
