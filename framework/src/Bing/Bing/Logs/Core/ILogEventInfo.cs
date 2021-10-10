using System;
using Bing.Logs.Core.Callers;
using Bing.Logs.Events;

namespace Bing.Logs.Core
{
    /// <summary>
    /// 日志事件信息
    /// </summary>
    public interface ILogEventInfo
    {
        /// <summary>
        /// 状态命名空间
        /// </summary>
        string StateNamespace { get; }

        /// <summary>
        /// 事件标识
        /// </summary>
        LogEventId EventId { get; }

        /// <summary>
        /// 时间戳
        /// </summary>
        DateTimeOffset Timestamp { get; }

        /// <summary>
        /// 日志事件级别
        /// </summary>
        string Level { get; }

        /// <summary>
        /// 异常
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// 日志调用者信息
        /// </summary>
        ILogCallerInfo CallerInfo { get; }

        /// <summary>
        /// 转换为日志事件
        /// </summary>
        LogEvent ToLogEvent();

        /// <summary>
        /// 转换为上下文日志事件
        /// </summary>
        IContextualLogEvent ToContextualLogEvent();
    }
}
