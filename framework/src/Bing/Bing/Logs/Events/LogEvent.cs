using System;
using System.Collections.Generic;
using Bing.Logs.Core;
using Bing.Logs.Core.Callers;
using Bing.Logs.ExtraSupports;

namespace Bing.Logs.Events
{
    /// <summary>
    /// 日志事件
    /// </summary>
    public class LogEvent : ILogEventInfo, IContextualLogEvent
    {
        /// <summary>
        /// 日志事件上下文
        /// </summary>
        private readonly LogEventContext _logEventContext;

        /// <summary>
        /// 日志事件上下文数据
        /// </summary>
        private readonly ContextData _contextData;

        /// <summary>
        /// 状态命名空间
        /// </summary>
        public string StateNamespace { get; }

        /// <summary>
        /// 事件标识
        /// </summary>
        public LogEventId EventId { get; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTimeOffset Timestamp => EventId.Timestamp;

        /// <summary>
        /// 日志事件级别
        /// </summary>
        public string Level { get; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// 日志调用者信息
        /// </summary>
        public ILogCallerInfo CallerInfo { get; }

        /// <summary>
        /// 日志事件上下文数据
        /// </summary>
        public ContextData ContextData => _contextData;

        /// <summary>
        /// 标签列表
        /// </summary>
        public IReadOnlyList<string> Tags => _logEventContext.Tags;

        /// <summary>
        /// 初始化一个<see cref="LogEvent"/>类型的实例
        /// </summary>
        internal LogEvent()
        {
            _logEventContext = null;
            CallerInfo = NullLogCallerInfo.Instance;
        }

        public LogEvent(
            string stateNamespace, 
            LogEventId eventId, 
            LogLevel level, 
            Exception exception,
            ILogCallerInfo callerInfo, 
            LogEventContext logEventContext, 
            ContextData contextData = null)
        {
            StateNamespace = stateNamespace;
            EventId = eventId;
            Level = level.ToString();
            Exception = exception;
            CallerInfo = callerInfo;
            _logEventContext = logEventContext ?? new LogEventContext();
            _contextData = contextData == null ? new ContextData() : new ContextData(contextData);
            if (exception != null && !_contextData.HasException())
                _contextData.SetException(exception);
            _contextData.ImportUpstreamContextData(_logEventContext.ExposeContextData());

        }

        /// <summary>
        /// 转换为日志事件
        /// </summary>
        public LogEvent ToLogEvent() => this;

        /// <summary>
        /// 转换为上下文日志事件
        /// </summary>
        public IContextualLogEvent ToContextualLogEvent() => this;
    }
}
