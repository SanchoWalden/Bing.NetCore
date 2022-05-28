using Bing.Logging;
using Bing.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bing.Internal
{
    /// <summary>
    /// 内部 - 服务集合 扩展
    /// </summary>
    internal static class InternalServiceCollectionExtensions
    {
        /// <summary>
        /// 注册核心服务
        /// </summary>
        /// <param name="services">服务集合</param>
        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddLocalization();
        }

        /// <summary>
        /// 注册核心Bing服务
        /// </summary>
        /// <param name="services">服务集合</param>
        internal static void AddCoreBingServices(this IServiceCollection services)
        {
            var moduleLoader = new ModuleLoader();
            services.TryAddSingleton<IInitLoggerFactory>(new DefaultInitLoggerFactory());
        }
    }
}
