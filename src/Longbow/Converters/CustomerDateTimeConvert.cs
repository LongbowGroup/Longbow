#if NETCore
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Longbow.Converters
{
    /// <summary>
    /// 转换 DateTime 类型数据为自定义格式的 Json 字符串
    /// </summary>
    public class CustomerDateTimeConvert : IsoDateTimeConverter
    {
        /// <summary>
        /// 默认构造函数 字符串默认为 "yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public CustomerDateTimeConvert() : this("yyyy-MM-dd HH:mm:ss")
        {

        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CustomerDateTimeConvert(string format)
        {
            DateTimeFormat = format;
        }

        /// <summary>
        /// 读取方法
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        /// <summary>
        /// 写入方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime v1 && v1 == DateTime.MinValue ||
                value is DateTimeOffset v2 && v2 == DateTimeOffset.MinValue) writer.WriteValue((object)null);
            else base.WriteJson(writer, value, serializer);
        }
    }
}
#endif
