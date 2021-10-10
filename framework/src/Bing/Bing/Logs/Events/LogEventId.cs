using System;

namespace Bing.Logs.Events
{
    /// <summary>
    /// 日志事件标识
    /// </summary>
    public partial class LogEventId
    {
        /// <summary>
        /// 默认事件标识
        /// </summary>
        private const int DefaultIntegerEventId = 0;

        /// <summary>
        /// 父日志事件标识
        /// </summary>
        private readonly LogEventId _parentEventId;

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTimeOffset Timestamp { get; }

        /// <summary>
        /// 标识
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 跟踪标识
        /// </summary>
        public string TraceId { get; }

        /// <summary>
        /// 业务跟踪标识
        /// </summary>
        public string BusinessTraceId { get; set; }

        /// <summary>
        /// 日志作用域跟踪标识
        /// </summary>
        public string LoggingScopeTraceId { get; set; }

        /// <summary>
        /// 日志事件名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 是否有父日志事件标识
        /// </summary>
        public bool HasParentEventId => _parentEventId != null;

        /// <summary>
        /// 父日志事件标识
        /// </summary>
        public LogEventId Parent => _parentEventId;

        /// <summary>
        /// 初始化一个<see cref="LogEventId"/>类型的实例
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="traceId">跟踪标识</param>
        internal LogEventId(string id, string name, string traceId)
        {
            Id = id;
            Name = name;
            TraceId = traceId;
            Timestamp = Now();
            _parentEventId = default;
        }

        /// <summary>
        /// 初始化一个<see cref="LogEventId"/>类型的实例
        /// </summary>
        /// <param name="eventId">日志事件标识</param>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        internal LogEventId(LogEventId eventId, string id, string name)
        {
            Id = id;
            Name = name;
            TraceId = eventId.TraceId;
            Timestamp = Now();
            _parentEventId = eventId;
        }

        /// <summary>
        /// 当前时间戳
        /// </summary>
        private static DateTimeOffset Now()
        {
            var _ = DateTime.Now;
            return new DateTimeOffset(_, TimeZoneInfo.Local.GetUtcOffset(_));
        }

        /// <summary>
        /// 获取事件标识
        /// </summary>
        public int GetIntegerEventId() => int.TryParse(Id, out var ret) ? ret : DefaultIntegerEventId;
    }
}
