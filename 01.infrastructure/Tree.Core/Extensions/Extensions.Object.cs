using AspectCore.Extensions.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tree.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     convert to safety string, remove the space at begin and end of string,
        ///     also it will be "" when the value is null
        /// </summary>
        /// <param name="input">input value</param>
        public static string ToSecureString(this object input)
        {
            return input?.ToString().Trim() ?? string.Empty;
        }
        /// <summary>
        /// 把对象类型转换为指定类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object CastTo(this object value, Type conversionType)
        {
            if (value == null)
            {
                return null;
            }

            if (conversionType.IsNullableType())
            {
                conversionType = conversionType.GetUnNullableType();
            }

            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString());
            }

            if (conversionType == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }

            return Convert.ChangeType(value, conversionType);
        }
        public static T CastTo<T>(this object value)
        {
            if (value == null && default(T) == null)
            {
                return default(T);
            }

            if (value != null && value.GetType() == typeof(T))
            {
                return (T)value;
            }

            var result = CastTo(value, typeof(T));
            return (T)result;
        }



    }
}
