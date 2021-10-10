using System;
using System.Threading;

namespace Bing.Logs.Trace
{
    /// <summary>
    /// 日志跟踪标识生成器
    /// </summary>
    public static class LogTraceIdGenerator
    {
        /// <summary>
        /// 当前日志跟踪标识生成器
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static AsyncLocal<ILogTraceIdGenerator> _currentTraceIdGenerator = new AsyncLocal<ILogTraceIdGenerator>();

        /// <summary>
        /// 作用域更新
        /// </summary>
        /// <param name="logTraceIdGenerator">日志跟踪标识生成器</param>
        public static void ScopedUpdate(ILogTraceIdGenerator logTraceIdGenerator)
        {
            if (logTraceIdGenerator is null)
                throw new ArgumentNullException(nameof(logTraceIdGenerator));
            _currentTraceIdGenerator.Value = logTraceIdGenerator;
        }

        /// <summary>
        /// 当前日志跟踪标识生成器
        /// </summary>
        public static ILogTraceIdGenerator Current => _currentTraceIdGenerator.Value ?? SystemTraceIdGenerator.Fallback;
    }
}
