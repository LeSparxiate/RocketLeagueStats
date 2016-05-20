using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop1.Classes
{
    public class Population
    {
        public enum Servers
        {
            A = -2,
            B = 0,
            C = 1,
            D = 2,
            E = 3,
            F = 4,
            G = 6,
            H = 7,
            I = 8,
            J = 9,
            K = 10,
            L = 11,
            M = 12,
            N = 13,
            O = 15,
            P = 16,
            Q = 17
        }

        public List<Servers> ServList;

        public List<uint> ServPop;
    }
}
