using System;
using System.Collections.Generic;

namespace EasyIpClientTest
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static double[] ToDoubleArray(this IList<int> list)
        {
            var resultList = new List<double>();
            foreach (var item in list)
            {
                double targetItem = item;
                resultList.Add(targetItem);
            }
            return resultList.ToArray();
        }

        public static long[] ToLongArray(this IList<int> list)
        {
            var resultList = new List<long>();
            foreach (var item in list)
            {
                long targetItem = item;
                resultList.Add(targetItem);
            }
            return resultList.ToArray();
        }

        public static short[] ToShortArray(this IList<int> list)
        {
            var resultList = new List<short>();
            foreach (var item in list)
            {
                short targetItem = (short)item;
                resultList.Add(targetItem);
            }
            return resultList.ToArray();
        }
    }
}
