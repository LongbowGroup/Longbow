namespace System.Net
{
    /// <summary>
    /// IPAddress 扩展操作类
    /// </summary>
    public static class IPAddressExtensions
    {
        /// <summary>
        /// 将 IPAddress 转换为 IPv4 格式字符串形式
        /// </summary>
        /// <param name="address">IPAddress 实例</param>
        /// <returns>字符串形式</returns>
        public static string ToIPv4String(this IPAddress address) => InternalIPAddressExtensions.ToIPv4String(address);
    }
}
