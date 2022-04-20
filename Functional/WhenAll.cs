using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Basic.Functional
{
    public class WhenAll
    {
        private readonly List<object> _doneResults = new List<object>();
        private Action<object[]> _onAllDoneHandler;

        public Action<object> NewHandler
        {
            get
            {
                int newIndex = _doneResults.Count;
                _doneResults.Add(null);
                return result =>
                {
                    _doneResults[newIndex] = result;
                    Handle();
                };
            }
        }

        private void Handle()
        {
            if (!_doneResults.Any(o => o == null))
            {
                _onAllDoneHandler(_doneResults.ToArray());
            }
        }

        public void OnDone<T>(Action<T[]> handler) where T : class
        {
            // to do: handling immediate finishes
            _onAllDoneHandler = args =>
            {
                handler(args.Cast<T>().ToArray());
            };
        }
    }
}
