namespace Bing.Logs.Trace
{
    /// <summary>
    /// 日志跟踪标识
    /// </summary>
    public static class LogTraceId
    {
        /// <summary>
        /// 获取日志跟踪标识
        /// </summary>
        public static string Get() => LogTraceIdGenerator.Current.Create();
    }
}
