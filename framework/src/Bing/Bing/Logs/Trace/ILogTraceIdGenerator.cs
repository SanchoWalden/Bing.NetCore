namespace Bing.Logs.Trace
{
    /// <summary>
    /// 日志跟踪标识生成器
    /// </summary>
    public interface ILogTraceIdGenerator
    {
        /// <summary>
        /// 创建
        /// </summary>
        string Create();
    }
}
