using System.Collections.Generic;

namespace Bing.Logs.Core
{
    /// <summary>
    /// 上下文日志事件
    /// </summary>
    public interface IContextualLogEvent
    {
        /// <summary>
        /// 标签列表
        /// </summary>
        IReadOnlyList<string> Tags { get; }
    }
}
