using Common.Basic.Functional;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Basic.Collections
{
    public static class EnumerableExtensions
    {
        public static void ForEach(this int index, Action action)
        {
            index = System.Math.Abs(index);
            for (int i = 0; i < index; i++)
                action();
        }

        public static void ForEach(this int index, Action<int> action)
        {
            index = System.Math.Abs(index);
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

        public static T FirstBefore<T>(this IEnumerable<T> source, int index)
        {
            var array = source.ToArray();
            if (array.Length <= 1)
                return default;

            if (index >= array.Length)
                return array.FirstBeforeLast();

            return array[index - 1];
        }

        public static T FirstBeforeLast<T>(this IEnumerable<T> source)
        {
            var array = source.ToArray();
            if (array.Length <= 1)
                return default;

            return array[array.Length - 2];
        }

        public static T FirstBeforeLastOrFirst<T>(this IEnumerable<T> source)
        {
            if (source.Count() == 0)
                return default;

            if (source.Count() < 2)
                return source.First();

            return source.FirstBeforeLast();
        }

        public static IEnumerable<T> TakeExceptLast<T>(this IEnumerable<T> source) => source.Take(source.Count() - 1);
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T item) => source.Except(new T[] { item });

        public static bool TryGetValue<T>(this IList<T> source, int index, out T value)
        {
            value = default;
            if (index < 0)
                return false;

            int count = source.Count();
            if (index >= count)
                return false;

            value = source.ToArray()[index];
            return true;
        }

        //public static IEnumerable<T> Flatten<T, R>(this IEnumerable<T> source, Func<T, R> recursion) where R : IEnumerable<T>
        //{
        //    if (source == null || recursion == null)
        //        return null;

        //    return source.SelectMany(x => 
        //    {
        //        var recursionValue = recursion(x).ToArray();
        //        if (recursionValue.IsNullOrEmpty())
        //            return Array.Empty<T>();

        //        return recursionValue.Flatten(recursion);
        //    })
        //    .Where(x => x != null)
        //    .ToArray();
        //}

        public static IEnumerable<TSource> Flatten<TSource>(
              this IEnumerable<TSource> source,
              Func<TSource, IEnumerable<TSource>> getChildrenFunction)
        {
            foreach (TSource element in source)
            {
                var children = getChildrenFunction(element);
                var childrenFlattened = children.Flatten(getChildrenFunction);
                source = source.Concat(childrenFlattened);
            }

            return source;
        }

        public static IEnumerable<Tuple<T1, T2>> ZipToTuples<T1, T2>(this IEnumerable<T1> e1, IEnumerable<T2> e2) =>
            e1.Zip(e2, (x, y) => new Tuple<T1, T2>(x, y));
    }
}
