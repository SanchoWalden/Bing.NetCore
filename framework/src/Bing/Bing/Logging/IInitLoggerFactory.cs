namespace Bing.Logging
{
    /// <summary>
    /// 初始化日志工厂
    /// </summary>
    public interface IInitLoggerFactory
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        IInitLogger<T> Create<T>();
    }
}
