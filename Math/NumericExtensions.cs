using System;

namespace Common.Domain.Collections
{
    public static class NumericExtensions
    {
        public static void IncreaseBy<T>(this ref T value, T byValue, T lowerLimit, T upperLimit, bool loop = true) where T : struct
        {
            dynamic dynamicValue = value;

            dynamicValue += byValue;
            if (dynamicValue > upperLimit)
                dynamicValue = loop ? lowerLimit : upperLimit;
                
            value = dynamicValue;
        }

        public static bool Equals<T>(this T first, T second, T tolerance) where T : struct
        {
            dynamic dynamicFirst = first;
            dynamic dynamicSecond = second;
            
            dynamic res = dynamicFirst - dynamicSecond;
            dynamic resAbs = Math.Abs(res);

            return resAbs <= tolerance;
        }
    }
}
