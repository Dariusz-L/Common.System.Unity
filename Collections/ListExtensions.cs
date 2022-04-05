using System;
using System.Collections.Generic;

namespace Common.Basic.Collections
{
    public static class ListExtensions
    {
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public static void SwapFirst<T>(this IList<T> list, int index) => list.Swap(index, 0);
        public static void SwapLast<T>(this IList<T> list, int index) => list.Swap(index, list.Count - 1);
        public static void SwapFirstAndLast<T>(this IList<T> list) => list.SwapLast(0);

        public static void MoveToLast<T>(this IList<T> list, int index)
        {
            list.Add(list[index]);
            list.RemoveAt(index);
        }

        public static void MoveFirstToLast<T>(this IList<T> list) => list.MoveToLast(0);

        public static void Insert<T>(this List<T> list, T item, int index)
        {
            index = index.Clamp(0, list.Count);
            list.Insert(index, item);
        }
    }
}
