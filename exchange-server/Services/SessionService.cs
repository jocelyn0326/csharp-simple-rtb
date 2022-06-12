using System.Collections.Generic;

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
        public void AddSession(string session_id)
        {
            SessionStatusDic.Add(session_id, true);
        }
        /// <summary>
        /// While post end_session
        /// </summary>
        public bool EndSession(string session_id)
        {
            bool result = false;
            if (SessionStatusDic.ContainsKey(session_id) && SessionStatusDic[session_id] == true)
            {
                SessionStatusDic[session_id] = false;
                result = true;
            }
            return result;
        }


    }
  
}
