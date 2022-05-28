using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Bing.Logging
{
    /// <summary>
    /// 初始化日志
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public interface IInitLogger<out T> : ILogger<T>
    {
        /// <summary>
        /// 初始化日志条目列表
        /// </summary>
        List<BingInitLogEntry> Entries { get; }
    }
}
