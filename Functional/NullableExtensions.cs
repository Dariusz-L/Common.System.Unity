﻿namespace Common.Domain.Functional
{
    public static class NullableExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
