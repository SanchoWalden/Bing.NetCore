using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Bing.Core.Modularity;
using Bing.Helpers;
using Bing.Reflection;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 文件夹 - 插件源
    /// </summary>
    public class FolderPlugInSource : IPlugInSource
    {
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string Folder { get; }

        /// <summary>
        /// 搜索选项
        /// </summary>
        public SearchOption SearchOption { get; set; }

        /// <summary>
        /// 过滤器
        /// </summary>
        public Func<string, bool> Filter { get; set; }

        /// <summary>
        /// 初始化一个<see cref="FolderPlugInSource"/>类型的实例
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <param name="searchOption">搜索选项</param>
        public FolderPlugInSource(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Check.NotNull(folder, nameof(folder));
            Folder = folder;
            SearchOption = searchOption;
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        public Type[] GetModules()
        {
            var modules = new List<Type>();
            foreach (var assembly in GetAssemblies())
            {
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

        /// <summary>
        /// 获取程序集列表
        /// </summary>
        private List<Assembly> GetAssemblies()
        {
            var assemblyFiles = AssemblyHelper.GetAssemblyFiles(Folder, SearchOption);
            if (Filter != null)
                assemblyFiles = assemblyFiles.Where(Filter);
            return assemblyFiles.Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).ToList();
        }
    }
}
