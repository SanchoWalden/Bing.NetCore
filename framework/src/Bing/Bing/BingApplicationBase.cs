using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bing.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bing
{
    public abstract class BingApplicationBase:IBingApplication
    {
        /// <summary>
        /// 启动模块类型
        /// </summary>
        /// <remarks>
        /// 应用程序的启动（入口）模块类型
        /// </remarks>
        public Type StartupModuleType { get; }

        /// <summary>
        /// 服务集合
        /// </summary>
        /// <remarks>
        /// 应用初始化后，无法向该集合继续添加新服务。
        /// </remarks>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 服务提供程序
        /// </summary>
        /// <remarks>
        /// 引用应用程序的根服务提供程序，在初始化应用程序之前不能使用这个。
        /// </remarks>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 模块列表
        /// </summary>
        public IReadOnlyList<IBingModuleDescriptor> Modules { get; }

        /// <summary>
        /// 是否已配置服务
        /// </summary>
        private bool _configuredServices;


        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <remarks>
        /// 调用模块中的 Pre / Post / ConfigureServicesAsync() 方法。
        /// </remarks>
        public Task ConfigureServicesAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        public Task ShutdownAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}
