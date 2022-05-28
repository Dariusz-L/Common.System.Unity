using Common.Basic.Collections;
using System;

namespace Common.Basic.Functional
{
    public static class FunctionalActions
    {
        public static void IfOkOrNot(Func<bool> condition, Action onTrue, Action onFalse)
        {
            if (condition())
                onTrue();
            else
                onFalse();
        }

        public static void IfOkOrNot(bool value, Action onTrue, Action onFalse) =>
            IfOkOrNot(() => value, onTrue, onFalse);

        public static Action<T1, T2> BranchAction<T1, T2>(params Action<T1, T2>[] actions) =>
            (arg1, arg2) => actions.ForEach(a => a?.Invoke(arg1, arg2));
    }
}
