using System.Threading.Tasks;

namespace Bing.Modularity
{
    /// <summary>
    /// 配置服务前
    /// </summary>
    public interface IPreConfigureServices
    {
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 该方法在 ConfigureServices 方法执行之前被调用。
        /// </remarks>
        Task PreConfigureServicesAsync(ServiceConfigurationContext context);

        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 该方法在 ConfigureServices 方法执行之前被调用。
        /// </remarks>
        void PreConfigureServices(ServiceConfigurationContext context);
    }
}
