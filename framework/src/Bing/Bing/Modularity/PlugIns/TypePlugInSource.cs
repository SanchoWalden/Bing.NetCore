using System;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 类型 - 插件源
    /// </summary>
    public class TypePlugInSource : IPlugInSource
    {
        /// <summary>
        /// 模块类型数组
        /// </summary>
        private readonly Type[] _moduleTypes;

        /// <summary>
        /// 初始化一个<see cref="TypePlugInSource"/>类型的实例
        /// </summary>
        /// <param name="moduleTypes">模块类型数组</param>
        public TypePlugInSource(params Type[] moduleTypes)
        {
            _moduleTypes = moduleTypes ?? Type.EmptyTypes;
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        public Type[] GetModules() => _moduleTypes;
    }
}
