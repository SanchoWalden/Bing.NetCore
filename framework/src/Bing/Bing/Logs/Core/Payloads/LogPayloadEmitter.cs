using System.Threading.Tasks;

namespace Bing.Logs.Core.Payloads
{
    /// <summary>
    /// 日志负载提交器
    /// </summary>
    public static class LogPayloadEmitter
    {
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="logPayloadSender">日志负载发送器</param>
        /// <param name="payload">日志负载</param>
        public static void Emit(ILogPayloadSender logPayloadSender, ILogPayload payload)
        {
            if (logPayloadSender is null || payload is null)
                return;
            Task.Factory.StartNew(async () => await logPayloadSender.SendAsync(payload));
        }
    }
}
