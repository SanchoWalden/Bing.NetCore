using System;
using System.IO;
using Bing.Helpers;

namespace Bing.Modularity.PlugIns
{
    /// <summary>
    /// 插件源列表(<see cref="PlugInSourceList"/>) 扩展
    /// </summary>
    public static class PlugInSourceListExtensions
    {
        /// <summary>
        /// 添加文件夹插件源
        /// </summary>
        /// <param name="list">插件源列表</param>
        /// <param name="folder">文件夹路径</param>
        /// <param name="searchOption">搜索选项</param>
        public static void AddFolder(this PlugInSourceList list, string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Check.NotNull(list, nameof(list));
            list.Add(new FolderPlugInSource(folder, searchOption));
        }

        /// <summary>
        /// 添加类型插件源
        /// </summary>
        /// <param name="list">插件源列表</param>
        /// <param name="moduleTypes">模块类型数组</param>
        public static void AddTypes(this PlugInSourceList list, params Type[] moduleTypes)
        {
            Check.NotNull(list, nameof(list));
            list.Add(new TypePlugInSource(moduleTypes));
        }

        /// <summary>
        /// 添加文件插件源
        /// </summary>
        /// <param name="list">插件源数组</param>
        /// <param name="filePaths">文件路径数组</param>
        public static void AddFiles(this PlugInSourceList list, params string[] filePaths)
        {
            Check.NotNull(list, nameof(list));
            list.Add(new FilePlugInSource(filePaths));
        }
    }
}
