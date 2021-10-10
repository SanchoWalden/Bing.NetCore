using System.Threading;
using System.Threading.Tasks;

namespace Bing.Logs.Core.Payloads
{
    /// <summary>
    /// 日志负载客户端
    /// </summary>
    public interface ILogPayloadClient
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="payload">日志负载</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task WriteAsync(ILogPayload payload, CancellationToken cancellationToken = default);
    }
}
