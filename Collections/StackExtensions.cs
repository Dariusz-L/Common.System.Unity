using System.Collections.Generic;

namespace Common.Domain.Collections
{
    public static class StackExtensions
    {
        public static void Pop<T>(this Stack<T> stack, int popCount)
        {
            if (popCount <= 0)
                return;

            if (popCount > stack.Count)
                popCount = stack.Count;

            for (int i = 0; i < popCount; i++)
                stack.Pop();
        }
    }
}
