using System;

namespace Bing.Modularity
{
    /// <summary>
    /// 模块依赖
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedTypesProvider
    {
        /// <summary>
        /// 当前模块的所依赖模块类型集合
        /// </summary>
        public Type[] DependedTypes { get; }

        /// <summary>
        /// 初始化一个<see cref="DependsOnAttribute"/>类型的实例
        /// </summary>
        /// <param name="dependedTypes">依赖模块类型集合</param>
        public DependsOnAttribute(params Type[] dependedTypes) => DependedTypes = dependedTypes ?? Type.EmptyTypes;

        /// <summary>
        /// 获取依赖类型
        /// </summary>
        public virtual Type[] GetDependedTypes() => DependedTypes;
    }
}
