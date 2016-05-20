using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop1.Classes
{
    /// <summary>
    /// Contain some usefull functions (parsing, ...)
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// Used to parse RL API results.
        /// </summary>
        /// <param name="toParse">Contain API result.</param>
        public static string[] ParseRequest(string toParse)
        {
            string[] lines = toParse.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }

        public static void ParsePopRequest(string toParse)
        {
            //PlaylistID
            //NumPlayers
            string[] ret = ParseRequest(toParse);
            string[] datas = null;
            string[] values = null;
            foreach (string oneLine in ret)
            {
                datas = oneLine.Split('&');
            }
            if (datas != null)
            {
                foreach (string oneParam in datas)
                {
                    values = oneParam.Split('=');
                    if (values != null && values.Count() == 2)
                    {
                        //                        values[1]
                    }
                }
            }
        }

        public static List<PlayerStats> ParseLeaderboardRequest(string toParse)
        {
            PlayerStats pStats = null;
            List<PlayerStats> pList = new List<PlayerStats>();
            string[] ret = ParseRequest(toParse);
            string[] datas = null;
            string[] values = null;
            foreach (string oneLine in ret)
            {
                datas = oneLine.Split('&');
                if (datas != null)
                {
                    pStats = new PlayerStats();
                    foreach (string oneParam in datas)
                    {
                        values = oneParam.Split('=');
                        if (values.Count() == 2 && values[0] != "LeaderboardID")
                        {
                            pStats.setValues(values[0], values[1]);
                        }
                    }
                    pList.Add(pStats);
                }
            }
            return pList;
        }

        public static void ParsePopulation(string toParse)
        {

        }
    }
}
