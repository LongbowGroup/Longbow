using System;
using System.ComponentModel;
using System.Reflection;

namespace Longbow
{
    /// <summary>
    /// 数据转化操作类
    /// </summary>
    public static class LgbConvert
    {
        /// <summary>
        /// 将数据类型转换为指定的数据类型，转换失败则将其初始化为默认值
        /// </summary>
        /// <typeparam name="T">返回的数据类型</typeparam>
        /// <param name="source">原数据</param>
        /// <param name="defaultValue">转化后的数据默认值</param>
        /// <returns>返回的数据类型实例值</returns>
        public static T ReadValue<T>(object source, T defaultValue = default)
        {
            if (source == null || source.ToString() == string.Empty || source == DBNull.Value) return defaultValue;

            return TryConvertValue(typeof(T), source.ToString(), out var result) ? (T)result : defaultValue;
        }

        private static bool TryConvertValue(Type type, string value, out object result)
        {
            result = null;
            if (type == typeof(object))
            {
                result = value;
                return true;
            }

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) return TryConvertValue(Nullable.GetUnderlyingType(type), value, out result);

            var converter = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(typeof(string)))
            {
                try
                {
                    result = converter.ConvertFromInvariantString(value);
                    return true;
                }
                catch { }
            }

            return false;
        }
    }
}
