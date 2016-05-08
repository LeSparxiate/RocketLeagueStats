using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Workshop1.Classes
{
    /// <summary>
    /// Used to perform requests to RL API.
    /// </summary>
    public class RLApi
    {
        /// <summary>
        /// Represent all Ranked GameTypes.
        /// </summary>
        public enum GameTypes
        {
            OVO = 10,
            TVT = 11,
            THVTHSOLO = 12,
            THVTH = 13
        }

        public string sessionID = "";

        /// <summary>
        /// Allow to get a SessionID to request API.
        /// </summary>
        private void getSessionID()
        {
            string[] parameters = new string[] { "PlayerName=", "PlayerID=1", "Platform=PS4", "BuildID=812023742", "AuthCode=0", "IssuerID=0" };
            string[] headers = new string[] { "LoginSecretKey" };
            string[] h_data = new string[] { "dUe3SE4YsR8B0c30E6r7F2KqpZSbGiVx" };

            HttpWebResponse ret = HttpRequest.PostRequest("https://psyonix-rl.appspot.com/auth/", parameters, headers, h_data);
            this.sessionID = ret.Headers.Get("sessionid");
        }

        public string getPopulation()
        {
            string ret = "";

            if (sessionID == "")
                this.getSessionID();

            string[] headers = new string[] { "SessionID", "CallProcKey", "Cache-Control", "Environment" };
            string[] h_data = new string[] { sessionID, "pX9pn8F4JnBpoO8Aa219QC6N7g18FJ0F", "no-cache", "Prod" };

            HttpWebResponse result = HttpRequest.PostRequest("https://psyonix-rl.appspot.com/Population/GetPopulation/", null, headers, h_data);

            ret = new StreamReader(result.GetResponseStream()).ReadToEnd();

            return ret;
        }

        /// <summary>
        /// Allow to get all Top Ranking in specified GameType.
        /// </summary>
        /// <param name="gt">GameType : 1v1, 2v2, 3v3 solo, 3v3</param>
        /// <returns>Response string</returns>
        public string getAllRanking(GameTypes gt)
        {
            string ret = "";

            if (sessionID == "")
                this.getSessionID();

            string[] parameters = new string[] { "Proc[]=GetSkillLeaderboard_v2", "P0P[]="+ (int)gt};
            string[] headers = new string[] { "SessionID", "CallProcKey", "Cache-Control", "Environment" };
            string[] h_data = new string[] { sessionID, "pX9pn8F4JnBpoO8Aa219QC6N7g18FJ0F", "no-cache", "Prod" };

            HttpWebResponse result = HttpRequest.PostRequest("https://psyonix-rl.appspot.com/callproc105/", parameters, headers, h_data);

            ret = new StreamReader(result.GetResponseStream()).ReadToEnd();

            return ret;
        }

        /// <summary>
        /// Allow to get a Steam User rank in specified GameType
        /// </summary>
        /// <param name="SteamPlayerID">Steam user ID</param>
        /// <param name="gt">GameType : 1v1, 2v2, 3v3 solo, 3v3</param>
        /// <returns>Response string</returns>
        public string getPlayerRankingSteam(string SteamPlayerID, GameTypes gt)
        {
            string ret = "";

            if (sessionID == "")
                this.getSessionID();

            string[] parameters = new string[] { "Proc[]=GetSkillLeaderboardValueForUser_v2Steam", "P0P[]="+SteamPlayerID, "P0P[]="+(int)gt };
            string[] headers = new string[] { "SessionID", "CallProcKey", "Cache-Control", "Environment" };
            string[] h_data = new string[] { sessionID, "pX9pn8F4JnBpoO8Aa219QC6N7g18FJ0F", "no-cache", "Prod" };

            HttpWebResponse result = HttpRequest.PostRequest("https://psyonix-rl.appspot.com/callproc105/", parameters, headers, h_data);

            ret = new StreamReader(result.GetResponseStream()).ReadToEnd();

            return ret;
        }
    }
}
