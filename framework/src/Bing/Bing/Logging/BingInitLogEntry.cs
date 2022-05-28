using System;
using Microsoft.Extensions.Logging;

namespace Bing.Logging
{
    /// <summary>
    /// 初始化日志条目
    /// </summary>
    public class BingInitLogEntry
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// 事件标识
        /// </summary>
        public EventId EventId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public object State { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 格式化函数
        /// </summary>
        public Func<object, Exception, string> Formatter { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message => Formatter(State, Exception);
    }
}
