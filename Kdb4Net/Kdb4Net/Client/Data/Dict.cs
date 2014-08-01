using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdb4Net.Client.Data
{
    public class Dict
    {
        public object x;
        public object y;

        public Dict(object X, object Y)
        {
            x = X;
            y = Y;
        }
    }
}
