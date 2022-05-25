using System;
using System.Threading.Tasks;
using Bing.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Core.Modularity
{
    /// <summary>
    /// Bing 模块
    /// </summary>
    public interface IBingModule
    {
        /// <summary>
        /// 模块级别。级别越小越先启动
        /// </summary>
        ModuleLevel Level { get; }

        /// <summary>
        /// 模块启动顺序。模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 是否已启用
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 添加服务。将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">服务集合</param>
        [Obsolete("请使用 ConfigureServices 或 ConfigureServicesAsync 方法进行配置服务")]
        IServiceCollection AddServices(IServiceCollection services);

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供程序</param>
        void UseModule(IServiceProvider provider);

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 配置模块和注册服务的主要方法。
        /// </remarks>
        Task ConfigureServicesAsync(ServiceConfigurationContext context);

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 配置模块和注册服务的主要方法。
        /// </remarks>
        void ConfigureServices(ServiceConfigurationContext context);
    }
}
