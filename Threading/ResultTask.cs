﻿using Common.Basic.Blocks;
using System;
using System.Threading.Tasks;

namespace Common.Basic.Threading
{
    public static class ResultTask
    {
        public static Task Run<TReturnValue>(
            Func<Task<Result<TReturnValue>>> function, Action<TReturnValue> onSuccess, Action onError = null)
        {
            return Task.Run(() =>
            {
                var res = function().GetAwaiter().GetResult();
                if (res.IsSuccess)
                    onSuccess(res.Get<TReturnValue>());
                else
                    onError?.Invoke();
            });
        }
    }
}
