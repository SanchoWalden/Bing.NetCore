using System;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 插件源
    /// </summary>
    public interface IPlugInSource
    {
        /// <summary>
        /// 获取模块列表
        /// </summary>
        Type[] GetModules();
    }
}
