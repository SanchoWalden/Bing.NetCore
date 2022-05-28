using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 插件源列表
    /// </summary>
    public class PlugInSourceList : List<IPlugInSource>
    {
        /// <summary>
        /// 获取全部模块列表
        /// </summary>
        /// <param name="logger">日志</param>
        internal Type[] GetAllModules(ILogger logger)
        {
            return this.SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies(logger))
                .Distinct()
                .ToArray();
        }
    }
}
