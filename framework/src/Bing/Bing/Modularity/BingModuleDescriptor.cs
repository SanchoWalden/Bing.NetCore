using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Bing.Core.Modularity;
using Bing.Helpers;

namespace Bing.Modularity
{
    /// <summary>
    /// Bing 模块描述符
    /// </summary>
    public class BingModuleDescriptor : IBingModuleDescriptor
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// 模块实例
        /// </summary>
        public IBingModule Instance { get; }

        /// <summary>
        /// 是否插件加载
        /// </summary>
        public bool IsLoadedAsPlugIn { get; }

        /// <summary>
        /// 模块依赖关系
        /// </summary>
        public IReadOnlyList<IBingModuleDescriptor> Dependencies => _dependencies.ToImmutableList();

        /// <summary>
        /// 模块依赖关系
        /// </summary>
        private readonly List<IBingModuleDescriptor> _dependencies;

        /// <summary>
        /// 初始化一个<see cref="BingModuleDescriptor"/>类型的实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <param name="isLoadedAsPlugin"></param>
        public BingModuleDescriptor(Type type, IBingModule instance, bool isLoadedAsPlugin)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            if (!type.GetTypeInfo().IsInstanceOfType(instance))
                throw new ArgumentException($"Given module instance ({instance.GetType().AssemblyQualifiedName}) is not an instance of given module type: {type.AssemblyQualifiedName}");

            Type = type;
            Assembly = type.Assembly;
            Instance = instance;
            IsLoadedAsPlugIn = isLoadedAsPlugin;

            _dependencies = new List<IBingModuleDescriptor>();
        }

        /// <summary>
        /// 添加依赖关系
        /// </summary>
        /// <param name="descriptor">模块描述符</param>
        public void AddDependency(IBingModuleDescriptor descriptor)
        {
            _dependencies.AddIfNotContains(descriptor);
        }

        /// <summary>
        /// 输出字符串
        /// </summary>
        public override string ToString()
        {
            return $"[BingModuleDescriptor {Type.FullName}]";
        }
    }
}
