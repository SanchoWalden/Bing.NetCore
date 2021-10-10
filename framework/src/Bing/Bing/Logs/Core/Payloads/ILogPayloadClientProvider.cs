namespace Bing.Logs.Core.Payloads
{
    /// <summary>
    /// 日志负载客户端提供程序
    /// </summary>
    public interface ILogPayloadClientProvider
    {
        /// <summary>
        /// 获取日志负载客户端
        /// </summary>
        ILogPayloadClient GetClient();
    }
}
