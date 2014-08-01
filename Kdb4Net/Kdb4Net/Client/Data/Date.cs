using System;

namespace Kdb4Net.Client.Data
{
    [Serializable]
    public class Date : IComparable
    {
        public static Date NULL = new Date(Int32.MinValue);

        public int i;

        public Date(int x)
        {
            i = x;
        }

        public Date(long x)
        {
            i = x == 0L ? Int32.MinValue :
                (int)(x / (long)8.64e11) - 730119;
        }

        public Date(DateTime z)
            : this(z.Ticks)
        {
        }

        public int CompareTo(object o)
        {
            if (o == null)
                return 1;
            var other = o as Date;
            if (other == null)
                return 1;

            return i.CompareTo(other.i);
        }

        public DateTime ToDateTime()
        {
            return i == -int.MaxValue ? DateTime.MinValue :
                   i == int.MaxValue ? DateTime.MaxValue :
                   new DateTime(i == Int32.MinValue ? 0L :
                        clampDT((long)8.64e11 * i + (long)8.64e11 * 730119));
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
            return i == Int32.MinValue ? String.Empty : ToDateTime().ToString("d");
        }

        private static long clampDT(long j)
        {
            return Math.Min(Math.Max(j, DateTime.MinValue.Ticks), DateTime.MaxValue.Ticks);
        }
    }
}
