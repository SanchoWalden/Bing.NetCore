using System;
using System.Threading.Tasks;
using Bing.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bing
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public interface IBingApplication : IModuleContainer, IDisposable
    {
        /// <summary>
        /// 启动模块类型
        /// </summary>
        /// <remarks>
        /// 应用程序的启动（入口）模块类型
        /// </remarks>
        Type StartupModuleType { get; }

        /// <summary>
        /// 服务集合
        /// </summary>
        /// <remarks>
        /// 应用初始化后，无法向该集合继续添加新服务。
        /// </remarks>
        IServiceCollection Services { get; }

        /// <summary>
        /// 服务提供程序
        /// </summary>
        /// <remarks>
        /// 引用应用程序的根服务提供程序，在初始化应用程序之前不能使用这个。
        /// </remarks>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <remarks>
        /// 调用模块中的 Pre / Post / ConfigureServicesAsync() 方法。
        /// </remarks>
        Task ConfigureServicesAsync();

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        Task ShutdownAsync();

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        void Shutdown();
    }
}
