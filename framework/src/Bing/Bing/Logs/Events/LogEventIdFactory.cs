using System;
using Bing.Logs.Trace;

namespace Bing.Logs.Events
{
    /// <summary>
    /// 日志事件标识工厂
    /// </summary>
    public static class LogEventIdFactory
    {
        /// <summary>
        /// 创建日志事件标识
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="traceId">跟踪标识</param>
        public static LogEventId Create(string id = null, string name = null, string traceId = null) => CreateOrUpdateCurrentEventId(id, name, traceId);

        /// <summary>
        /// 创建日志事件标识
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="traceId">跟踪标识</param>
        public static LogEventId Create(int id, string name, string traceId = null) => CreateOrUpdateCurrentEventId(id, name, traceId);

        /// <summary>
        /// 创建日志事件标识
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="traceId">跟踪标识</param>
        public static LogEventId Create(long id, string name, string traceId = null) => CreateOrUpdateCurrentEventId(id, name, traceId);

        /// <summary>
        /// 创建日志事件标识
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="traceId">跟踪标识</param>
        public static LogEventId Create(Guid id, string name, string traceId = null) => CreateOrUpdateCurrentEventId(id, name, traceId);

        /// <summary>
        /// 创建日志事件标识
        /// </summary>
        /// <param name="track">日志跟踪</param>
        public static LogEventId Create(LogTrack track) => CreateOrUpdateCurrentEventId(track);

        /// <summary>
        /// 创建或更新当前日志事件标识
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="traceId">跟踪标识</param>
        private static LogEventId CreateOrUpdateCurrentEventId<T>(T id, string name, string traceId) =>
            CreateOrUpdateCurrentEventId(id.ToString(), name, traceId);

        /// <summary>
        /// 创建或更新当前日志事件标识
        /// </summary>
        /// <param name="track">日志跟踪</param>
        private static LogEventId CreateOrUpdateCurrentEventId(LogTrack track) => CreateOrUpdateCurrentEventId(track.Id, track.Name, track.BusinessTraceId);

        /// <summary>
        /// 创建或更新当前日志事件标识
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="traceId">跟踪标识</param>
        private static LogEventId CreateOrUpdateCurrentEventId(string id, string name, string traceId)
        {
            /*
             * 1、 当 LogEventId.Current 为空的时候（此时 NeedUpdateCurrentValue 为 true），将压入一个新的 LogEventId 实例作为 root，NeedUpdateCurrentValue 标记为 false，并返回
             * 2、 当 LogEventId.Current 不为空，则取出 Current，并以之作为 Parent，生成一个新的 LogEventId 实例，不压如入 Current，返回
             * 3、 当 BeginScope，则将 NeedUpdateCurrentValue 标记为 true;
             *         在下一次 TouchCurrentEventId 时将第一个 eventId 压入，将原 current 作为 Parent
             */
            name ??= string.Empty;
            if (string.IsNullOrWhiteSpace(id))
                id = Guid.NewGuid().ToString();
            var eventId = LogEventId.Current;
            var realTraceId = MakeRealTraceId(eventId?.TraceId, traceId);
            var businessTraceId = MakeBusinessTraceId(eventId?.BusinessTraceId, traceId, realTraceId);

            var instance = eventId != null ? new LogEventId(eventId, id, name) { BusinessTraceId = businessTraceId } : new LogEventId(id, name, realTraceId) { BusinessTraceId = businessTraceId };

            if (LogEventId.NeedUpdateCurrentValue)
            {
                LogEventId.Current = instance;
                LogEventId.NeedUpdateCurrentValue = false;
            }
            return instance;
        }

        /// <summary>
        /// 构建真实跟踪标识
        /// </summary>
        /// <param name="traceIdFromCurrentEventId">跟踪标识</param>
        /// <param name="traceId">跟踪标识</param>
        private static string MakeRealTraceId(string traceIdFromCurrentEventId, string traceId)
        {
            return @return(string.IsNullOrWhiteSpace(traceIdFromCurrentEventId) ? traceId : traceIdFromCurrentEventId);

            string @return(string value) => string.IsNullOrWhiteSpace(value) ? LogTraceId.Get() : value;
        }

        /// <summary>
        /// 构建业务跟踪标识
        /// </summary>
        /// <param name="businessTraceIdFromCurrentEventId">业务跟踪标识</param>
        /// <param name="traceId">跟踪标识</param>
        /// <param name="realTraceId">真实跟踪标识</param>
        private static string MakeBusinessTraceId(string businessTraceIdFromCurrentEventId, string traceId, string realTraceId)
        {
            var biz = string.IsNullOrWhiteSpace(traceId) ? businessTraceIdFromCurrentEventId : traceId;
            return string.IsNullOrWhiteSpace(biz) ? realTraceId : biz;
        }
    }
}
