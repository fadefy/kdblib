using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdb4Net.Client.Data
{
    [Serializable]
    public class Month : IComparable
    {
        public static Month NULL = new Month(Int32.MinValue);

        public int i;

        public Month(int x)
        {
            i = x;
        }

        public int CompareTo(object o)
        {
            if (o == null)
                return 1;
            var other = o as Month;
            if (other == null)
                return 1;

            return i.CompareTo(other.i);
        }

        public override bool Equals(object o)
        {
            return CompareTo(o) == 0;
        }

        public override int GetHashCode()
        {
            return i;
        }

        public override string ToString()
        {
            int m = 24000 + i, y = m / 12;

            return i == Int32.MinValue ? String.Empty
                : i2(y / 100) + i2(y % 100) + "-" + i2(1 + m % 12);
        }

        private static string i2(int i)
        {
            return String.Format("{0:00}", i);
        }
    }
}
