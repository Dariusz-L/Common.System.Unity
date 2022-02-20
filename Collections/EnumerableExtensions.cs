using Common.Domain.Functional;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Domain.Collections
{
    public static class EnumerableExtensions
    {
        public static void ForEach(this int index, Action action)
        {
            index = Math.Abs(index);
            for (int i = 0; i < index; i++)
                action();
        }

        public static void ForEach(this int index, Action<int> action)
        {
            index = Math.Abs(index);
            for (int i = 0; i < index; i++)
                action(i);
        }

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

        public static void ForEachN<T>(
            this IEnumerable<T> enumerable, int stepStart, int step, Action<T> onStep, Action<T> onOther = null)
        {
            if (step < 1)
                return;

            var array = enumerable.ToArray();
            int count = array.Length;

            for (int i = 0; i < stepStart; i++)
                onOther(array[i]);

            for (int i = stepStart; i < count; i += step)
            {
                onStep(array[i]);
                for (int j = i + 1; j < i + step && j < count; j++)
                    onOther(array[j]);
            }
        }

        public static IEnumerable<T> ForEachReversed<T>(this IEnumerable<T> enumerable, Action<T> itemAction)
        {
            var list = enumerable.ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (item != null)
                    itemAction(item);
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
