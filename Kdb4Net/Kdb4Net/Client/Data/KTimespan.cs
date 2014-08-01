using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdb4Net.Client.Data
{
    [Serializable]
    public class KTimespan : IComparable
    {
        public static KTimespan NULL = new KTimespan(Int64.MinValue);

        public TimeSpan t;

        public KTimespan(long x)
        {
            t = new TimeSpan(x == Int64.MinValue ? Int64.MinValue : x / 100);
        }

        private KTimespan()
        {
        }
        public int CompareTo(object o)
        {
            if (o == null)
                return 1;
            var other = o as KTimespan;
            if (other == null)
                return 1;

            return t.CompareTo(other.t);
        }

        public override bool Equals(object o)
        {
            return CompareTo(o) == 0;
        }

        public override int GetHashCode()
        {
            return t.GetHashCode();
        }

        public override string ToString()
        {
            return qn(t) ? "" : t.ToString();
        }

        public static bool qn(object x)
        {
            int t = -KTypes.Value(x);
            return t > 4 && x.Equals(KTypes.Null(t));
        }
    }
}
