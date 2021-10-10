using System;
using System.Collections.Generic;
using Bing.Logs.Events;

namespace Bing.Logs.Core.Payloads
{
    /// <summary>
    /// 日志负载
    /// </summary>
    public interface ILogPayload : IEnumerable<LogEvent>
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 来源类型
        /// </summary>
        Type SourceType { get; }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="logEvent">日志事件</param>
        void Add(LogEvent logEvent);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="logEvents">日志事件集合</param>
        void AddRange(IEnumerable<LogEvent> logEvents);

        /// <summary>
        /// 导出
        /// </summary>
        ILogPayload Export();

        /// <summary>
        /// 重置
        /// </summary>
        void Reset();

        /// <summary>
        /// 清空
        /// </summary>
        void Clear();
    }
}
