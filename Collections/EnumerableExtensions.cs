using Common.Domain.Functional;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Domain.Collections
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> itemAction)
        {
            foreach (var item in enumerable)
            {
                if (item != null)
                    itemAction(item);
            }

            return enumerable;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> itemAction)
        {
            int i = 0;
            foreach (var item in enumerable)
            {
                if (item != null)
                    itemAction(item, i);

                i++;
            }

            return enumerable;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.IsNull() || enumerable.IsEmpty();
        }

        public static bool IsNullOrEmptyOrNotOne<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.IsNullOrEmpty() || !enumerable.IsOneCount();
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random randomRange = null)
        {
            randomRange = randomRange ?? new Random();

            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = randomRange.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
    }
}
