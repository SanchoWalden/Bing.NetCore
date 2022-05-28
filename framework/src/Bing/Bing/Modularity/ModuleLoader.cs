using System;
using System.Collections.Generic;
using System.Linq;
using Bing.Core.Modularity;
using Bing.Helpers;
using Bing.Modularity.PlugIns;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Modularity
{
    /// <summary>
    /// 模块加载器
    /// </summary>
    public class ModuleLoader : IModuleLoader
    {
        /// <summary>
        /// 加载模块列表
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">启动模块类型</param>
        /// <param name="plugInSources">插件源</param>
        public IBingModuleDescriptor[] LoadModules(IServiceCollection services, Type startupModuleType, PlugInSourceList plugInSources)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(startupModuleType, nameof(startupModuleType));
            Check.NotNull(plugInSources, nameof(plugInSources));

            var modules = GetDescriptors(services, startupModuleType, plugInSources);
            modules = SortByDependency(modules, startupModuleType);
            return modules.ToArray();
        }

        /// <summary>
        /// 获取模块描述符列表
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">启动模块类型</param>
        /// <param name="plugInSources">插件源列表</param>
        private List<IBingModuleDescriptor> GetDescriptors(IServiceCollection services, Type startupModuleType, PlugInSourceList plugInSources)
        {
            var modules = new List<BingModuleDescriptor>();
            FillModules(modules,services,startupModuleType,plugInSources);
            SetDependencies(modules);
            return modules.Cast<IBingModuleDescriptor>().ToList();
        }

        /// <summary>
        /// 填充模块
        /// </summary>
        /// <param name="modules">模块描述符列表</param>
        /// <param name="services">服务集合</param>
        /// <param name="startupModuleType">启动模块类型</param>
        /// <param name="plugInSources">插件源列表</param>
        protected virtual void FillModules(List<BingModuleDescriptor> modules, IServiceCollection services, Type startupModuleType, PlugInSourceList plugInSources)
        {
            var logger = services.GetInitLogger<BingApplicationBase>();

            // 通过启动模块类型获取依赖的所有模块
            foreach (var moduleType in BingModuleHelper.FindAllModuleTypes(startupModuleType,logger))
                modules.Add(CreateModuleDescriptor(services, moduleType));

            // 插件模块
            foreach (var moduleType in plugInSources.GetAllModules(logger))
            {
                if(modules.Any(m=>m.Type==moduleType))
                    continue;
                modules.Add(CreateModuleDescriptor(services, moduleType, isLoadedAsPlugIn: true));
            }
        }

        /// <summary>
        /// 设置依赖项
        /// </summary>
        /// <param name="modules">模块描述符列表</param>
        protected virtual void SetDependencies(List<BingModuleDescriptor> modules)
        {
            foreach (var module in modules)
                SetDependencies(modules, module);
        }

        /// <summary>
        /// 按依赖进行排序
        /// </summary>
        /// <param name="modules">模块描述符列表</param>
        /// <param name="startupModuleType">启动模块类型</param>
        protected virtual List<IBingModuleDescriptor> SortByDependency(List<IBingModuleDescriptor> modules, Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(m => m.Dependencies);
            sortedModules.MoveItem(m => m.Type == startupModuleType, modules.Count - 1);
            return sortedModules;
        }

        /// <summary>
        /// 创建模块描述符
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="moduleType">模块类型</param>
        /// <param name="isLoadedAsPlugIn">是否插件加载</param>
        protected virtual BingModuleDescriptor CreateModuleDescriptor(IServiceCollection services, Type moduleType, bool isLoadedAsPlugIn = false)
        {
            return new BingModuleDescriptor(moduleType, CreateAndRegisterModule(services, moduleType), isLoadedAsPlugIn);
        }

        /// <summary>
        /// 创建并注册模块
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="moduleType">模块类型</param>
        protected virtual IBingModule CreateAndRegisterModule(IServiceCollection services, Type moduleType)
        {
            var module = (IBingModule)Activator.CreateInstance(moduleType);
            services.AddSingleton(moduleType, module);
            return module;
        }

        /// <summary>
        /// 设置依赖项
        /// </summary>
        /// <param name="modules">模块描述符列表</param>
        /// <param name="module">模块描述符</param>
        protected virtual void SetDependencies(List<BingModuleDescriptor> modules, BingModuleDescriptor module)
        {
            foreach (var dependedModuleType in BingModuleHelper.FindDependedModuleTypes(module.Type))
            {
                var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);
                if (dependedModule == null)
                    throw new BingFrameworkException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + module.Type.AssemblyQualifiedName);
                module.AddDependency(dependedModule);
            }
        }
    }
}
