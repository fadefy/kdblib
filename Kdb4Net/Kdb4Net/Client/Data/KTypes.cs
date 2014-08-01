using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kdb4Net.Client.Data
{
    public class KTypes
    {
        private static Dictionary<Type, int> typeValues;
        private static object[] NU =
        {
            null,
            false,
            new Guid(),
            null,
            (byte)0,
            Int16.MinValue,
            Int32.MinValue,
            Int64.MinValue,
            (Single)Double.NaN,
            Double.NaN,
            ' ',
            String.Empty,
            new DateTime(0),
            Month.NULL,
            Date.NULL,
            new DateTime(0),
            KTimespan.NULL,
            Minute.NULL,
            Second.NULL,
            new TimeSpan(Int32.MinValue)
        };

        static KTypes()
        {
            typeValues = new Dictionary<Type, int>()
            {
                { typeof(bool), -1 },
                { typeof(Guid), -2 },
                { typeof(byte), -4 },
                { typeof(short), -5 },
                { typeof(int), -6 },
                { typeof(long), -7 },
                { typeof(float), -8 },
                { typeof(double), -9 },
                { typeof(char), -10 },
                { typeof(string), -11 },
                { typeof(DateTime), -12 },
                { typeof(Month), -13 },
                { typeof(Date), -14 },
                { typeof(KTimespan), -16 },
                { typeof(Minute), -17 },
                { typeof(Second), -18 },
                { typeof(TimeSpan), -19 },
                { typeof(bool[]), 1 },
                { typeof(Guid[]), 2 },
                { typeof(byte[]), 4 },
                { typeof(short[]), 5 },
                { typeof(int[]), 6 },
                { typeof(long[]), 7 },
                { typeof(float[]), 8 },
                { typeof(double[]), 9 },
                { typeof(char[]), 10 },
                { typeof(DateTime[]), 12 },
                { typeof(KTimespan[]), 16 },
                { typeof(TimeSpan), 19 },
                { typeof(Flip), 98 },
                { typeof(Dict), 99 },
            };
        }

        public static int Value(object target)
        {
            var type = 0;
            typeValues.TryGetValue(target.GetType(), out type);

            return type;
        }

        public static object Null(int typeValue)
        {
            return NU[typeValue];
        }

        public static object Null(object value)
        {
            return Null(Value(value));
        }
    }
}
