using System.Collections.Generic;

namespace Bing.Modularity
{
    /// <summary>
    /// 模块容器
    /// </summary>
    public interface IModuleContainer
    {
        /// <summary>
        /// 模块列表
        /// </summary>
        IReadOnlyList<IBingModuleDescriptor> Modules { get; }
    }
}
