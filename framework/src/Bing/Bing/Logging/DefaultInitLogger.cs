using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Bing.Logging
{
    /// <summary>
    /// 默认的初始化日志
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class DefaultInitLogger<T> : IInitLogger<T>
    {
        /// <summary>
        /// 初始化日志条目列表
        /// </summary>
        public List<BingInitLogEntry> Entries { get; }

        /// <summary>
        /// 初始化一个<see cref="DefaultInitLogger{T}"/>类型的实例
        /// </summary>
        public DefaultInitLogger()
        {
            Entries = new List<BingInitLogEntry>();
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <typeparam name="TState">条目类型</typeparam>
        /// <param name="logLevel">日志级别</param>
        /// <param name="eventId">事件标识</param>
        /// <param name="state">条目</param>
        /// <param name="exception">异常</param>
        /// <param name="formatter">格式化函数</param>
        public virtual void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Entries.Add(new BingInitLogEntry
            {
                LogLevel = logLevel,
                EventId = eventId,
                State = state,
                Exception = exception,
                Formatter = (s, e) => formatter((TState)s, e)
            });
        }

        /// <summary>
        /// 是否启用日志
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        public virtual bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        /// <summary>
        /// 开始逻辑操作的作用域
        /// </summary>
        /// <typeparam name="TState">条目类型</typeparam>
        /// <param name="state">条目</param>
        public virtual IDisposable BeginScope<TState>(TState state) => NullDisposable.Instance;
    }
}
