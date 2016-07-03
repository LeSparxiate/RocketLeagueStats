using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Workshop1.Classes
{
    public class Steam
    {
        public static string getIdByName(string name)
        {
            string steamUrl = ConfigurationManager.AppSettings["steamUrl"] + name + ConfigurationManager.AppSettings["steamXMLEndpoint"];
            string[] parameters = new string[] {  };
            string[] headers = new string[] {  };
            string[] h_data = new string[] {  };
            string steamId = null;

            try
            {
                HttpWebResponse ret = HttpRequest.PostRequest(steamUrl, parameters, headers, h_data);
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(ret.GetResponseStream(), encoding))
                {
                    string xml = reader.ReadToEnd();
                    dynamic steamRes = DynamicXml.Parse(xml);
                    steamId = steamRes.steamID64;
                }

                if (steamId == null)
                    throw new Exception("\nSteamId is NULL.");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "\nError when trying to get SteamId");
            }
            return steamId;
        }
    }
}
