using exchange_server.Models.SessionModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace exchange_server.Services
{
    public class SessionService
    {
        Dictionary<string, bool> SessionStatusDic;
        public SessionService()
        {
            if (SessionStatusDic == null) {
                SessionStatusDic = new Dictionary<string, bool>();
            } 
        }
        /// <summary>
        /// While post init_session
        /// </summary>
        /// <param name="session_id"></param>
        internal void AddSession(string session_id)
        {
            SessionStatusDic.Add(session_id, true);
        }
        /// <summary>
        /// While post end_session
        /// </summary>
        internal bool EndSession(string session_id)
        {
            bool result = false;
            if (SessionStatusDic.ContainsKey(session_id) && SessionStatusDic[session_id] == true)
            {
                SessionStatusDic[session_id] = false;
                result = true;
            }
            return result;
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

        internal bool CheckSessionUniqueness(string session_id)
        {
            if (SessionStatusDic.ContainsKey(session_id)){
                return false;
            }
            return true;
        }

        internal Dictionary<string, bool> GetCurrentSessions() {
            return this.SessionStatusDic;
        }

    }
  
}
