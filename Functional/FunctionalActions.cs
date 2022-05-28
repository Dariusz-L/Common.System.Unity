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
    }
}
