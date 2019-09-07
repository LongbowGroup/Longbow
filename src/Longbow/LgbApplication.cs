using System.Diagnostics;

namespace Longbow
{
    /// <summary>
    /// 应用程序操作类。
    /// </summary>
    public static class LgbApplication
    {
        /// <summary>
        /// 判断应用程序域中是否已经有此程序集的实例。
        /// </summary>
        /// <param name="processName">The process to be find.</param>
        /// <returns>已经运行一个实例时返回真，否则返回假。</returns>
        public static bool HasInstance(string processName = null)
        {
            var count = 0;
            if (string.IsNullOrEmpty(processName))
            {
                // 判断自身
                processName = Process.GetCurrentProcess().ProcessName;
                count = 1;
            }
            var ps = Process.GetProcessesByName(processName);
            return ps.Length > count;
        }
    }
}
