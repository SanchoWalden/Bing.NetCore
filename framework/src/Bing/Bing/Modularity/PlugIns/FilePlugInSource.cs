using System;
using System.Collections.Generic;
using System.Runtime.Loader;
using Bing.Core.Modularity;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 文件 - 插件源
    /// </summary>
    public class FilePlugInSource : IPlugInSource
    {
        /// <summary>
        /// 文件路径数组
        /// </summary>
        public string[] FilePaths { get; }

        /// <summary>
        /// 初始化一个<see cref="FilePlugInSource"/>类型的实例
        /// </summary>
        /// <param name="filePaths">文件路径数组</param>
        public FilePlugInSource(params string[] filePaths)
        {
            FilePaths = filePaths ?? new string[0];
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        public Type[] GetModules()
        {
            var modules = new List<Type>();
            foreach (var filePath in FilePaths)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (BingModule.IsBingModule(type))
                            modules.AddIfNotContains(type);
                    }
                }
                catch (Exception e)
                {
                    throw new BingFrameworkException("Could not get module types from assembly: " + assembly.FullName, e);
                }
            }

            return modules.ToArray();
        }
    }
}
