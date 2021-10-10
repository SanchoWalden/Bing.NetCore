using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bing.Logs.Core.Payloads
{
    /// <summary>
    /// 日志负载发送器
    /// </summary>
    public sealed class LogPayloadSender : ILogPayloadSender
    {
        /// <summary>
        /// 日志负载客户端提供程序集合
        /// </summary>
        private readonly IEnumerable<ILogPayloadClientProvider> _logPayloadClientProviders;

        /// <summary>
        /// 初始化一个<see cref="LogPayloadSender"/>类型的实例
        /// </summary>
        /// <param name="logPayloadClientProviders">日志负载客户端提供程序集合</param>
        public LogPayloadSender(IEnumerable<ILogPayloadClientProvider> logPayloadClientProviders) =>
            _logPayloadClientProviders = logPayloadClientProviders ?? throw new ArgumentNullException(nameof(logPayloadClientProviders));

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="payload">日志负载</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task SendAsync(ILogPayload payload, CancellationToken cancellationToken = default)
        {
            foreach (var provider in _logPayloadClientProviders)
                await provider.GetClient().WriteAsync(payload, cancellationToken);
        }
    }
}
