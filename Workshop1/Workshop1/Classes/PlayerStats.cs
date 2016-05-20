using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop1.Classes
{
    public class PlayerStats
    {
        public enum Rank
        {
            UNRANKED = 0,
            PROSPECT1 = 1,
            PROSPECT2 = 2,
            PROSPECT3 = 3,
            PROSPECT4 = 4,
            CHALLENGER1 = 5,
            CHALLENGER2 = 6,
            CHALLENGER3 = 7,
            CHALLENGER4 = 8,
            RISINGSTAR = 9,
            ALLSTAR = 10,
            SUPERSTAR = 11,
            CHAMPION1 = 12,
            CHAMPION2 = 13,
            CHAMPION3 = 14,
            CHAMPION4 = 15,
        }

        public int rank { get; set; }

        public string UserName { get; set; }

        public float MMR { get; set; }

        public Rank Division { get; set; }

        public string Platform { get; set; }

        public string SteamID { get; set; }

        public void setValues(string param, string value)
        {
            if (param == "UserName")
                this.UserName = value;
            else if (param == "MMR")
                this.MMR = float.Parse(value, CultureInfo.InvariantCulture);
            else if (param == "Value")
                this.Division = (Rank)Int32.Parse(value);
            else if (param == "Platform")
                this.Platform = value;
            else if (param == "SteamID")
                this.SteamID = value;

        }
    }
}
