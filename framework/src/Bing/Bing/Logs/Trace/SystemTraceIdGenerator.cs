using System;

namespace Bing.Logs.Trace
{
    /// <summary>
    /// 系统日志跟踪标识生成器
    /// </summary>
    public class SystemTraceIdGenerator : ILogTraceIdGenerator
    {
        /// <summary>
        /// 创建
        /// </summary>
        public string Create() => Guid.NewGuid().ToString("N");

        /// <summary>
        /// 回调跟踪标识生成器
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static readonly SystemTraceIdGenerator _fallbackTraceIdGenerator = new SystemTraceIdGenerator();

        /// <summary>
        /// 回调
        /// </summary>
        internal static ILogTraceIdGenerator Fallback => _fallbackTraceIdGenerator;
    }
}
