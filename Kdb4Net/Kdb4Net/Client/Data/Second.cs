using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdb4Net.Client.Data
{
    [Serializable]
    public class Second : IComparable
    {
        public static Second NULL = new Second(Int32.MinValue);

        public int i;

        public Second(int x)
        {
            i = x;
        }

        private Second()
        {
        }
        public int CompareTo(object o)
        {
            if (o == null)
                return 1;
            var other = o as Second;
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
            return i == Int32.MinValue ? String.Empty : 
                new Minute(i / 60).ToString() + ':' + i2(i % 60);
        }

        private static string i2(int i)
        {
            return String.Format("{0:00}", i);
        }
    }
}
