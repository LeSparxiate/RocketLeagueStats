using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Workshop1.Classes
{
    /// <summary>
    /// Used to perform requests to RL API.
    /// </summary>
    public class RLApi
    {
        //Store sessionID (5hrs lifetime)
        public string sessionID = "";

        //Get all API Keys from App.config
        private string buildID = ConfigurationManager.AppSettings["BuildID"];
        private string loginSecretKey = ConfigurationManager.AppSettings["LoginSecretKey"];
        private string callProcKey = ConfigurationManager.AppSettings["CallProcKey"];

        //Get all API Urls from App.config
        private string authURL = ConfigurationManager.AppSettings["auth"];
        private string callProcURL = ConfigurationManager.AppSettings["callproc"];
        private string populationURL = ConfigurationManager.AppSettings["population"];

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

        /// <summary>
        /// Allow to get a SessionID to request API.
        /// </summary>
        public void getSessionID()
        {
            string[] parameters = new string[] { "PlayerName=", "PlayerID=1", "Platform=PS4", "BuildID="+ buildID, "AuthCode=0", "IssuerID=0" };
            string[] headers = new string[] { "LoginSecretKey" };
            string[] h_data = new string[] { loginSecretKey };

            try
            {
                HttpWebResponse ret = HttpRequest.PostRequest(authURL, parameters, headers, h_data);
                this.sessionID = ret.Headers.Get("sessionid");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message+"\nError when trying to get SessionID");
            }

        }

        /// <summary>
        /// Allow to get servers population.
        /// </summary>
        /// <returns>String containing servers population</returns>
        public string getPopulation()
        {
            string ret = "";

            string[] headers = new string[] { "SessionID", "CallProcKey", "Cache-Control", "Environment" };
            string[] h_data = new string[] { sessionID, callProcKey, "no-cache", "Prod" };

            try
            {
                if (sessionID == "")
                    this.getSessionID();
                HttpWebResponse result = HttpRequest.PostRequest(populationURL, null, headers, h_data);
                ret = new StreamReader(result.GetResponseStream()).ReadToEnd();

                //PARSE POPULATION
                Tools.ParsePopulation(ret);

                return ret;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message+ "\nError when trying to get server Population");
            }
        }

        /// <summary>
        /// Allow to get all Top Ranking in specified GameType.
        /// </summary>
        /// <param name="gt">GameType : 1v1, 2v2, 3v3 solo, 3v3</param>
        /// <returns>Response string</returns>
        public List<PlayerStats> getAllRanking(GameTypes gt)
        {
            string ret = "";

            string[] parameters = new string[] { "Proc[]=GetSkillLeaderboard_v2", "P0P[]="+ (int)gt};
            string[] headers = new string[] { "SessionID", "CallProcKey", "Cache-Control", "Environment" };
            string[] h_data = new string[] { sessionID, callProcKey, "no-cache", "Prod" };

            try
            {
                if (sessionID == "")
                    this.getSessionID();
                HttpWebResponse result = HttpRequest.PostRequest(callProcURL, parameters, headers, h_data);
                ret = new StreamReader(result.GetResponseStream()).ReadToEnd();

                List<PlayerStats> pList = Tools.ParseLeaderboardRequest(ret);

                return pList;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message+ "\nError when trying to get game type Leaderboard");
            }
        }

        /// <summary>
        /// Allow to get a Steam User rank in specified GameType.
        /// </summary>
        /// <param name="SteamPlayerID">Steam user ID</param>
        /// <param name="gt">GameType : 1v1, 2v2, 3v3 solo, 3v3</param>
        /// <returns>Response string</returns>
        public List<PlayerStats> getPlayerRankingSteam(string SteamPlayerID, GameTypes gt)
        {
            string ret = "";

            string[] parameters = new string[] { "Proc[]=GetSkillLeaderboardValueForUser_v2Steam", "P0P[]="+SteamPlayerID, "P0P[]="+(int)gt };
            string[] headers = new string[] { "SessionID", "CallProcKey", "Cache-Control", "Environment" };
            string[] h_data = new string[] { sessionID, callProcKey, "no-cache", "Prod" };

            try
            {
                if (sessionID == "")
                    this.getSessionID();
                HttpWebResponse result = HttpRequest.PostRequest(callProcURL, parameters, headers, h_data);
                ret = new StreamReader(result.GetResponseStream()).ReadToEnd();

                List<PlayerStats> pRank = Tools.ParseLeaderboardRequest(ret);
                return pRank;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message+ "\nError when trying to get Steam Player game type rank");
            }
        }

        /// <summary>
        /// Allow to get ALL Steam Player ranks.
        /// </summary>
        /// <param name="SteamPlayerID">Steam user ID</param>
        /// <returns>String tab containing each game type rank</returns>
        public PlayerStats[][] getAllPlayerRankingSteam(string SteamPlayerID)
        {
            List<PlayerStats[]> pStats = new List<PlayerStats[]>();

            try
            {
                pStats.Add((this.getPlayerRankingSteam(SteamPlayerID, GameTypes.OVO).ToArray()));
                pStats.Add((this.getPlayerRankingSteam(SteamPlayerID, GameTypes.TVT).ToArray()));
                pStats.Add((this.getPlayerRankingSteam(SteamPlayerID, GameTypes.THVTHSOLO).ToArray()));
                pStats.Add((this.getPlayerRankingSteam(SteamPlayerID, GameTypes.THVTH).ToArray()));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message+ "\nError when trying to get all Steam Player ranks");
            }

            return pStats.ToArray();
        }

        /// <summary>
        /// Allow to get EVERY leaderboards
        /// </summary>
        /// <returns>PlayerStats tab conaining each leaderboard as a PlayerStats tab</returns>
        public PlayerStats[][] getAllGameTypesRanking()
        {
            List<PlayerStats[]> leaderBoards = new List<PlayerStats[]>();

            try
            {
                leaderBoards.Add((this.getAllRanking(GameTypes.OVO)).ToArray());
                leaderBoards.Add((this.getAllRanking(GameTypes.TVT)).ToArray());
                leaderBoards.Add((this.getAllRanking(GameTypes.THVTHSOLO)).ToArray());
                leaderBoards.Add((this.getAllRanking(GameTypes.THVTH)).ToArray());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message+ "\nError when trying to get all Leaderboards");
            }

            return leaderBoards.ToArray();
        }
    }
}
