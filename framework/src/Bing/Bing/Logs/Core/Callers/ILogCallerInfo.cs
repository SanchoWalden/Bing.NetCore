﻿namespace Bing.Logs.Core.Callers
{
    /// <summary>
    /// 日志调用者信息
    /// </summary>
    public interface ILogCallerInfo
    {
        /// <summary>
        /// 类名
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// 方法名
        /// </summary>
        string MethodName { get; }

        /// <summary>
        /// 文件路径
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// 行号
        /// </summary>
        int LineNumber { get; }
    }
}
