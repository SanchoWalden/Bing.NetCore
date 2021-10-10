namespace Bing.Logs.Core.Callers
{
    /// <summary>
    /// 日志调用者信息
    /// </summary>
    public readonly struct LogCallerInfo : ILogCallerInfo
    {
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// 行号
        /// </summary>
        public int LineNumber { get; }

        /// <summary>
        /// 初始化一个<see cref="LogCallerInfo"/>类型的实例
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="lineNumber">行号</param>
        public LogCallerInfo(string className, string methodName, string filePath = null, int lineNumber = 0)
        {
            ClassName = className;
            MethodName = methodName;
            FilePath = filePath;
            LineNumber = lineNumber;
        }
    }
}
