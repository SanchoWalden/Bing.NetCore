using System.Collections.Generic;
using Bing.Collections;
using Bing.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Modularity
{
    /// <summary>
    /// 服务配置上下文
    /// </summary>
    public class ServiceConfigurationContext
    {
        /// <summary>
        /// 服务集合
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 配置字典
        /// </summary>
        public IDictionary<string, object> Items { get; }

        /// <summary>
        /// 获取或设置存储的任意命名对象。
        /// 在服务注册阶段，用于模块之间的共享。
        /// </summary>
        /// <param name="key">缓存键</param>
        public object this[string key]
        {
            get => Items.GetOrDefault(key);
            set => Items[key] = value;
        }

        /// <summary>
        /// 初始化一个<see cref="ServiceConfigurationContext"/>类型的实例
        /// </summary>
        /// <param name="services">服务集合</param>
        public ServiceConfigurationContext(IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
            Items = new Dictionary<string, object>();
        }
    }
}
