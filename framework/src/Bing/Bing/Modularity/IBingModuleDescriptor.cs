using System;
using System.Collections.Generic;
using System.Reflection;
using Bing.Core.Modularity;

namespace Bing.Modularity
{
    /// <summary>
    /// Bing 模块描述符
    /// </summary>
    public interface IBingModuleDescriptor
    {
        /// <summary>
        /// 类型
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 模块实例
        /// </summary>
        IBingModule Instance { get; }

        /// <summary>
        /// 是否插件加载
        /// </summary>
        bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 模块依赖关系
        /// </summary>
        IReadOnlyList<IBingModuleDescriptor> Dependencies { get; }
    }
}
