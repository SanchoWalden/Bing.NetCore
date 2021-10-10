using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bing.Logs.Events;

namespace Bing.Logs.Core.Payloads
{
    /// <summary>
    /// 日志负载
    /// </summary>
    public class LogPayload : ILogPayload
    {
        /// <summary>
        /// 日志事件列表
        /// </summary>
        private List<LogEvent> LogEvents { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 来源类型
        /// </summary>
        public Type SourceType { get; }

        /// <summary>
        /// 初始化一个<see cref="LogPayload"/>类型的实例
        /// </summary>
        /// <param name="sourceType">来源类型</param>
        /// <param name="name">名称</param>
        /// <param name="events">日志事件集合</param>
        public LogPayload(Type sourceType, string name, IEnumerable<LogEvent> events)
        {
            if (events == null)
                throw new ArgumentNullException(nameof(events));
            LogEvents = events.ToList();
            SourceType = sourceType;
            Name = name;
        }

        /// <summary>
        /// 初始化一个<see cref="LogPayload"/>类型的实例
        /// </summary>
        /// <param name="sourceType">来源类型</param>
        /// <param name="name">名称</param>
        /// <param name="events">日志事件集合</param>
        public LogPayload(Type sourceType, string name, params LogEvent[] events) : this(sourceType, name, (IEnumerable<LogEvent>)events)
        {
        }

        public IEnumerator<LogEvent> GetEnumerator() => LogEvents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="logEvent">日志事件</param>
        public void Add(LogEvent logEvent)
        {
            if (logEvent == null)
                return;
            LogEvents.Add(logEvent);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="logEvents">日志事件集合</param>
        public void AddRange(IEnumerable<LogEvent> logEvents)
        {
            if (logEvents == null || !logEvents.Any())
                return;
            LogEvents.AddRange(logEvents.ToList());
        }

        /// <summary>
        /// 导出
        /// </summary>
        public ILogPayload Export()
        {
            var ret = new LogPayload(SourceType, Name, LogEvents);
            Reset();
            return ret;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset() => LogEvents = Enumerable.Empty<LogEvent>().ToList();

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear() => LogEvents.Clear();
    }
}
