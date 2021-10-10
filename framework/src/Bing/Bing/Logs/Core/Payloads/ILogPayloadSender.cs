using System.Threading;
using System.Threading.Tasks;

namespace Bing.Logs.Core.Payloads
{
    /// <summary>
    /// 日志负载发送器
    /// </summary>
    public interface ILogPayloadSender
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="payload">日志负载</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task SendAsync(ILogPayload payload, CancellationToken cancellationToken = default);
    }
}
