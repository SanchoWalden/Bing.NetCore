using System.Threading.Tasks;

namespace Bing.Modularity
{
    /// <summary>
    /// 配置服务后
    /// </summary>
    public interface IPostConfigureServices
    {
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 该方法在 ConfigureServices 方法执行之后被调用。
        /// </remarks>
        Task PostConfigureServicesAsync(ServiceConfigurationContext context);

        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 该方法在 ConfigureServices 方法执行之后被调用。
        /// </remarks>
        void PostConfigureServices(ServiceConfigurationContext context);
    }
}
