using System.Collections.Generic;

namespace Common.Basic.UMVC
{
    public sealed class Navigator : INavigator
    {
        private Stack<INavigatable> _stack = new Stack<INavigatable>();

        public void Push(INavigatable navigatable, bool stashFirst = false)
        {
            bool unstashPrevious = true;
            // If last in stack is same as passed, then replace
            if (_stack.Count > 0 && navigatable.Equals(_stack.Peek()))
                PopInternal(unstashPrevious = false);

            if (stashFirst)
                OnStash();

            if (navigatable.OnPush())
            {
                if (!stashFirst)
                    OnStash();

                _stack.Push(navigatable);
            }

            void OnStash()
            {
                if (_stack.Count > 0 && unstashPrevious)
                    _stack.Peek().OnStash(navigatable);
            }
        }

        public void PushLast()
        {
            if (_stack.Count == 0)
                return;

            Push(_stack.Peek());
        }

        public void Pop(bool unstashPrevious)
        {
            if (_stack.Count <= 1)
                return;

            PopInternal(unstashPrevious);
        }

        public INavigatable GetLast()
        {
            return _stack.Peek();
        }

        private void PopInternal(bool unstashPrevious)
        {
            _stack.Peek().OnPop();

            var poped = _stack.Pop();
            if (unstashPrevious)
                _stack.Peek().OnUnstash(poped);
        }
    }
}
