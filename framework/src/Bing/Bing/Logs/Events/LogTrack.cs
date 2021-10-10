namespace Bing.Logs.Events
{
    /// <summary>
    /// 日志跟踪
    /// </summary>
    public readonly struct LogTrack
    {
        /// <summary>
        /// 跟踪标识
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 跟踪名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 业务跟踪标识
        /// </summary>
        public string BusinessTraceId { get; }

        /// <summary>
        /// 初始化一个<see cref="LogTrack"/>类型的实例
        /// </summary>
        /// <param name="id">跟踪标识</param>
        public LogTrack(string id)
        {
            Id = id;
            Name = string.Empty;
            BusinessTraceId = string.Empty;
        }

        /// <summary>
        /// 初始化一个<see cref="LogTrack"/>类型的实例
        /// </summary>
        /// <param name="id">跟踪标识</param>
        /// <param name="name">跟踪名称</param>
        public LogTrack(string id, string name)
        {
            Id = id;
            Name = name;
            BusinessTraceId = string.Empty;
        }

        /// <summary>
        /// 初始化一个<see cref="LogTrack"/>类型的实例
        /// </summary>
        /// <param name="id">跟踪标识</param>
        /// <param name="name">跟踪名称</param>
        /// <param name="businessTraceId">业务跟踪标识</param>
        public LogTrack(string id, string name, string businessTraceId)
        {
            Id = id;
            Name = name;
            BusinessTraceId = businessTraceId;
        }

        /// <summary>
        /// 创建日志跟踪实例
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="id">标识</param>
        public static LogTrack Create<T>(T id) => new LogTrack(id.ToString());

        /// <summary>
        /// 创建日志跟踪实例
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        public static LogTrack Create<T>(T id, string name) => new LogTrack(id.ToString(), name);

        /// <summary>
        /// 创建日志跟踪实例
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="businessTraceId">业务跟踪标识</param>
        public static LogTrack Create<T>(T id, string name, string businessTraceId) => new LogTrack(id.ToString(), name, businessTraceId);
    }
}
