using System;
using Bing.DependencyInjection;
using Bing.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务集合 - 预配置 扩展
    /// </summary>
    public static class ServiceCollectionPreConfigureExtensions
    {
        /// <summary>
        /// 预配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="optionsAction">配置操作</param>
        public static IServiceCollection PreConfigure<TOptions>(this IServiceCollection services, Action<TOptions> optionsAction)
        {
            services.GetPreConfigureActions<TOptions>().Add(optionsAction);
            return services;
        }

        /// <summary>
        /// 执行预配置操作
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="services">服务集合</param>
        public static TOptions ExecutePreConfiguredActions<TOptions>(this IServiceCollection services)
            where TOptions : new()
        {
            return services.ExecutePreConfiguredActions(new TOptions());
        }

        /// <summary>
        /// 执行预配置操作
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="options">选项</param>
        public static TOptions ExecutePreConfiguredActions<TOptions>(this IServiceCollection services, TOptions options)
        {
            services.GetPreConfigureActions<TOptions>().Configure(options);
            return options;
        }

        /// <summary>
        /// 获取预先配置操作列表
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="services">服务集合</param>
        public static PreConfigureActionList<TOptions> GetPreConfigureActions<TOptions>(this IServiceCollection services)
        {
            var actionList = services.GetSingletonInstanceOrNull<IObjectAccessor<PreConfigureActionList<TOptions>>>()?.Value;
            if (actionList == null)
            {
                actionList = new PreConfigureActionList<TOptions>();
                services.AddObjectAccessor(actionList);
            }

            return actionList;
        }
    }
}
