using System;
using System.Linq;
using Bing.Helpers;
using Microsoft.Extensions.Logging;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 插件源(<see cref="IPlugInSource"/>) 扩展
    /// </summary>
    public static class PlugInSourceExtensions
    {
        /// <summary>
        /// 获取模块的全部依赖项
        /// </summary>
        /// <param name="plugInSource">插件源</param>
        /// <param name="logger">日志</param>
        public static Type[] GetModulesWithAllDependencies(this IPlugInSource plugInSource, ILogger logger)
        {
            Check.NotNull(plugInSource, nameof(plugInSource));

            return plugInSource
                .GetModules()
                .SelectMany(type => BingModuleHelper.FindAllModuleTypes(type, logger))
                .Distinct()
                .ToArray();
        }
    }
}
