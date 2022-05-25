using System;
using Bing.Helpers;

namespace Bing
{
    /// <summary>
    /// 应用程序关闭上下文
    /// </summary>
    public class ApplicationShutdownContext
    {
        /// <summary>
        /// 服务提供程序
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 初始化一个<see cref="ApplicationShutdownContext"/>类型的实例
        /// </summary>
        /// <param name="serviceProvider">服务提供程序</param>
        public ApplicationShutdownContext(IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            ServiceProvider = serviceProvider;
        }
    }
}
