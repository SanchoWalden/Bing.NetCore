namespace Bing.Logs.Core.Callers
{
    /// <summary>
    /// 空日志调用者信息
    /// </summary>
    public readonly struct NullLogCallerInfo : ILogCallerInfo
    {
        /// <summary>
        /// 空日志调用者信息实例
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static readonly NullLogCallerInfo Instance = new NullLogCallerInfo();

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName => null;

        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName => null;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath => null;

        /// <summary>
        /// 行号
        /// </summary>
        public int LineNumber => 0;
    }
}
