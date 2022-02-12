using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Basic
{
    public static class ResultExtensions
    {
        public static Result ToResult(this IEnumerable<Result> subResults)
        {
            return Result.Create(subResults);
        }

        public static Task<Result> ToResultTask(this IEnumerable<Result> subResults)
        {
            return Result.CreateTask(subResults);
        }

        public static Task<Result> ToResultTask(this Result result)
        {
            return Result.CreateTask(result);
        }

        public static Result<T> ToResult<T>(this IEnumerable<Result> subResults)
        {
            return Result<T>.Create(subResults);
        }

        public static Task<Result<T>> ToResultTask<T>(this IEnumerable<Result> subResults)
        {
            return Result<T>.CreateTask(subResults);
        }

        public static Result<T> ToResult<T>(this Result result)
        {
            return Result<T>.Create(result);
        }

        public static Task<Result<T>> ToResultTask<T>(this Result result)
        {
            return Result<T>.CreateTask(result);
        }

        public static Result ToResult(this object obj)
        {
            return Result.Success(obj);
        }

        public static Result<T> ToResult<T>(this T obj)
        {
            return Result<T>.Success(obj);
        }

        public static Task<Result<T>> ToResultTask<T>(this T obj)
        {
            return Task.FromResult(Result<T>.Success(obj));
        }

        public static Type GetTypeValue<TType>(this Result result)
        {
            return result.GetValues<Type>().Where(t => t == typeof(TType)).FirstOrDefault();
        }
    }
}
