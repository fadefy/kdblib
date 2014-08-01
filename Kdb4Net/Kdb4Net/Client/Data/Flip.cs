using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdb4Net.Client.Data
{
    public class Flip
    {
        public string[] x;
        public object[] y;

        public Flip(Dict X)
        {
            x = (string[])X.x;
            y = (object[])X.y;
        }

        public object at(string s)
        {
            return y[Array.IndexOf(x, s)];
        }
    }
}
