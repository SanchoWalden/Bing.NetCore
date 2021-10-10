namespace Bing.Logs.Events
{
    /// <summary>
    /// 日志事件接收器
    /// </summary>
    public interface ILogEventSink
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        LogLevel Level{ get; set; }
    }
}
