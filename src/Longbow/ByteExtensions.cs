#if NETCore
using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Byte 操作扩展类
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// SpanSplit 操作扩展方法
        /// </summary>
        /// <param name="source">源数组</param>
        /// <param name="splitStr">分隔符字节数组 分割规则作为整体</param>
        /// <returns>分割后的字符数组</returns>
        public static List<byte[]> SpanSplit(this byte[] source, byte[] splitStr)
        {
            var ret = new List<byte[]>();
            if (source == null || source.Length == 0) return new List<byte[]>();
            if (splitStr == null || splitStr.Length == 0) return new List<byte[]>() { source };

            var sourceSpan = source.AsSpan();
            var splitSpan = splitStr.AsSpan();

            do
            {
                var n = sourceSpan.IndexOf(splitSpan);
                if (n == -1)
                {
                    ret.Add(sourceSpan.ToArray());
                    break;
                }

                ret.Add(sourceSpan.Slice(0, n).ToArray());
                sourceSpan = sourceSpan.Slice(Math.Min(sourceSpan.Length, n + splitSpan.Length));
            }
            while (sourceSpan.Length > 0);
            return ret;
        }

        /// <summary>
        /// SpanSplitAny 操作扩展类
        /// </summary>
        /// <param name="source">源数组</param>
        /// <param name="splitStr">分隔符字节数组 分割规则是任意一个</param>
        /// <returns>分割后的字符数组</returns>
        public static List<byte[]> SpanSplitAny(this byte[] source, byte[] splitStr)
        {
            var ret = new List<byte[]>();
            if (source == null || source.Length == 0) return ret;
            if (splitStr == null || splitStr.Length == 0) { ret.Add(source); return ret; }

            var sourceSpan = source.AsSpan();
            var splitSpan = splitStr.AsSpan();

            do
            {
                var n = sourceSpan.IndexOfAny(splitSpan);
                if (n == -1)
                {
                    ret.Add(sourceSpan.ToArray());
                    break;
                }
                if (n == 0)
                {
                    sourceSpan = sourceSpan.Slice(1);
                    continue;
                }

                ret.Add(sourceSpan.Slice(0, n).ToArray());
                sourceSpan = sourceSpan.Slice(n);
            }
            while (sourceSpan.Length > 0);
            return ret;
        }
    }
}
#endif
