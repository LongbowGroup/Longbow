using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// String 操作扩展类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Null Empty 判断
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>Null/Empty时返回真</returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

#if NETCore
        /// <summary>
        /// 获得 当前操作系统目录分隔符的路径
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceOSPlatformPath(this string str) => OSPlatformPathExtenstions.GetOSPlatformPath(str);

        /// <summary>
        /// SpanSplit 扩展方法
        /// </summary>
        /// <param name="source">源数组</param>
        /// <param name="splitStr">分隔符数组 分割规则作为整体</param>
        /// <param name="stringSplitOptions">StringSplitOptions 选项</param>
        /// <returns>分割后的字符串数组</returns>
        public static List<string> SpanSplit(this string source, string splitStr = null, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            var ret = new List<string>();
            if (source.IsNullOrEmpty()) return ret;
            if (splitStr.IsNullOrEmpty()) splitStr = Environment.NewLine;

            var sourceSpan = source.AsSpan();
            var splitSpan = splitStr.AsSpan();

            do
            {
                var n = sourceSpan.IndexOf(splitSpan);
                if (n == -1) n = sourceSpan.Length;

                ret.Add(stringSplitOptions == StringSplitOptions.None
                    ? sourceSpan.Slice(0, n).ToString()
                    : sourceSpan.Slice(0, n).Trim().ToString());
                sourceSpan = sourceSpan.Slice(Math.Min(sourceSpan.Length, n + splitSpan.Length));
            }
            while (sourceSpan.Length > 0);
            return ret;
        }

        /// <summary>
        /// SpanSplit 扩展方法
        /// </summary>
        /// <param name="source">源数组</param>
        /// <param name="splitStr">分隔符数组 分割规则是任意一个</param>
        /// <param name="stringSplitOptions">StringSplitOptions 选项</param>
        /// <returns>分割后的字符串数组</returns>
        public static List<string> SpanSplitAny(this string source, string splitStr, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            var ret = new List<string>();
            if (source.IsNullOrEmpty()) return ret;
            if (splitStr.IsNullOrEmpty()) { ret.Add(source); return ret; }

            var sourceSpan = source.AsSpan();
            var splitSpan = splitStr.AsSpan();

            do
            {
                var n = sourceSpan.IndexOfAny(splitSpan);
                if (n == -1) n = sourceSpan.Length;

                if (n > 0) ret.Add(stringSplitOptions == StringSplitOptions.None
                     ? sourceSpan.Slice(0, n).ToString()
                     : sourceSpan.Slice(0, n).Trim().ToString());
                sourceSpan = sourceSpan.Slice(Math.Min(sourceSpan.Length, n + 1));
            }
            while (sourceSpan.Length > 0);
            return ret;
        }
#endif
    }
}
