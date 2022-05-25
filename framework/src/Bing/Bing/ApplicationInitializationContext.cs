﻿using System;
using Bing.DependencyInjection;
using Bing.Helpers;

namespace Bing
{
    /// <summary>
    /// 应用程序初始化上下文
    /// </summary>
    public class ApplicationInitializationContext : IServiceProviderAccessor
    {
        /// <summary>
        /// 服务提供程序
        /// </summary>
        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 初始化一个<see cref="ApplicationInitializationContext"/>类型的实例
        /// </summary>
        /// <param name="serviceProvider">服务提供程序</param>
        public ApplicationInitializationContext(IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            ServiceProvider = serviceProvider;
        }
    }
}
