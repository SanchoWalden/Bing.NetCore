using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bing.Extensions;
using Bing.Modularity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.Core.Modularity
{
    /// <summary>
    /// Bing 模块基类
    /// </summary>
    public abstract class BingModule :
        IBingModule,
        IOnPreApplicationInitialization,
        IOnApplicationInitialization,
        IOnPostApplicationInitialization,
        IOnApplicationShutdown,
        IPreConfigureServices,
        IPostConfigureServices
    {
        /// <summary>
        /// 服务配置上下文
        /// </summary>
        private ServiceConfigurationContext _serviceConfigurationContext;

        /// <summary>
        /// 服务配置上下文
        /// </summary>
        protected internal ServiceConfigurationContext ServiceConfigurationContext
        {
            get
            {
                if (_serviceConfigurationContext == null)
                    throw new BingFrameworkException($"{nameof(ServiceConfigurationContext)} is only available in the {nameof(ConfigureServices)}, {nameof(PreConfigureServices)} and {nameof(PostConfigureServices)} methods.");
                return _serviceConfigurationContext;
            }
            internal set => _serviceConfigurationContext = value;
        }

        /// <summary>
        /// 是否跳过自动服务注册
        /// </summary>
        protected internal bool SkipAutoServiceRegistration { get; protected set; }

        /// <summary>
        /// 模块级别。级别越小越先启动
        /// </summary>
        public virtual ModuleLevel Level => ModuleLevel.Business;

        /// <summary>
        /// 模块启动顺序。模块启动的顺序先按级别启动，同一级别内部再按此顺序启动，
        /// 级别默认为0，表示无依赖，需要在同级别有依赖顺序的时候，再重写为>0的顺序值
        /// </summary>
        public virtual int Order => 0;

        /// <summary>
        /// 是否已启用
        /// </summary>
        public virtual bool Enabled { get; protected set; }

        /// <summary>
        /// 添加服务。将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="services">服务集合</param>
        public virtual IServiceCollection AddServices(IServiceCollection services) => services;

        /// <summary>
        /// 应用模块服务
        /// </summary>
        /// <param name="provider">服务提供程序</param>
        public virtual void UseModule(IServiceProvider provider) => Enabled = true;

        /// <summary>
        /// 获取当前模块的依赖模块类型
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        internal Type[] GetDependModuleTypes(Type moduleType = null)
        {
            if (moduleType == null)
                moduleType = GetType();
            var dependAttrs = moduleType.GetAttributes<DependsOnModuleAttribute>(true).ToList();
            if (dependAttrs.Count == 0)
                return new Type[0];
            var dependTypes = new List<Type>();
            foreach (var dependAttr in dependAttrs)
            {
                var moduleTypes = dependAttr.DependedModuleTypes;
                if (moduleTypes.Length == 0)
                    continue;
                dependTypes.AddRange(moduleTypes);
                foreach (var type in moduleTypes)
                    dependTypes.AddRange(GetDependModuleTypes(type));
            }
            return dependTypes.Distinct().ToArray();
        }

        #region PreConfigureServices(配置服务前)

        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 该方法在 ConfigureServices 方法执行之前被调用。
        /// </remarks>
        public virtual Task PreConfigureServicesAsync(ServiceConfigurationContext context)
        {
            PreConfigureServices(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 该方法在 ConfigureServices 方法执行之前被调用。
        /// </remarks>
        public virtual void PreConfigureServices(ServiceConfigurationContext context)
        {
        }

        #endregion

        #region ConfigureServices(配置服务)

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 配置模块和注册服务的主要方法。
        /// </remarks>
        public virtual Task ConfigureServicesAsync(ServiceConfigurationContext context)
        {
            ConfigureServices(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 配置模块和注册服务的主要方法。
        /// </remarks>
        public virtual void ConfigureServices(ServiceConfigurationContext context)
        {
        }

        #endregion

        #region PostConfigureServices(配置服务后)

        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 该方法在 ConfigureServices 方法执行之后被调用。
        /// </remarks>
        public virtual Task PostConfigureServicesAsync(ServiceConfigurationContext context)
        {
            PostConfigureServices(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context">服务配置上下文</param>
        /// <remarks>
        /// 该方法在 ConfigureServices 方法执行之后被调用。
        /// </remarks>
        public virtual void PostConfigureServices(ServiceConfigurationContext context)
        {
        }

        #endregion

        #region PreApplicationInitialization(应用程序启动前)

        /// <summary>
        /// 应用程序启动前
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法在 OnApplicationInitialization 方法执行之前被调用。
        /// 在这个阶段，可以从依赖注入中解析服务，因为服务已经被初始化。
        /// </remarks>
        public virtual Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            OnPreApplicationInitialization(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 应用程序启动前
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法在 OnApplicationInitialization 方法执行之前被调用。
        /// 在这个阶段，可以从依赖注入中解析服务，因为服务已经被初始化。
        /// </remarks>
        public virtual void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
        }

        #endregion

        #region OnApplicationInitialization(应用程序启动)

        /// <summary>
        /// 应用程序启动
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法用来配置 ASP.NET Core 请求管道并初始化你的服务。
        /// </remarks>
        public virtual Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            OnApplicationInitialization(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 应用程序启动
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法用来配置 ASP.NET Core 请求管道并初始化你的服务。
        /// </remarks>
        public virtual void OnApplicationInitialization(ApplicationInitializationContext context)
        {
        }

        #endregion

        #region OnPostApplicationInitialization(应用程序启动后)

        /// <summary>
        /// 应用程序启动后
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法在 OnApplicationInitialization 方法执行之后被调用。
        /// </remarks>
        public virtual Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            OnPostApplicationInitialization(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 应用程序启动后
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法在 OnApplicationInitialization 方法执行之后被调用。
        /// </remarks>
        public virtual void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
        }

        #endregion

        #region OnApplicationShutdown(应用程序关闭)

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context">应用关闭上下文</param>
        /// <remarks>
        /// 根据需要自己实现模块的关闭逻辑。
        /// </remarks>
        public virtual Task OnApplicationShutdownAsync(ApplicationShutdownContext context)
        {
            OnApplicationShutdown(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context">应用关闭上下文</param>
        /// <remarks>
        /// 根据需要自己实现模块的关闭逻辑。
        /// </remarks>
        public virtual void OnApplicationShutdown(ApplicationShutdownContext context)
        {
        }

        #endregion

        #region PreConfigure(预配置)

        /// <summary>
        /// 预配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="configureOptions">选项操作</param>
        protected void PreConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PreConfigure(configureOptions);
        }

        #endregion

        #region Configure(配置)

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="configureOptions">选项绑定操作</param>
        protected void Configure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(configureOptions);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="name">配置名称</param>
        /// <param name="configureOptions">选项绑定操作</param>
        protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure(name, configureOptions);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="configuration">配置</param>
        protected void Configure<TOptions>(IConfiguration configuration)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="configuration">配置</param>
        /// <param name="configureBinder">选项绑定操作</param>
        protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(configuration, configureBinder);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="name">配置名称</param>
        /// <param name="configuration">配置</param>
        protected void Configure<TOptions>(string name, IConfiguration configuration)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.Configure<TOptions>(name, configuration);
        }

        #endregion

        #region PostConfigure(后配置)

        /// <summary>
        /// 后配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="configureOptions">选项操作</param>
        protected void PostConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PostConfigure(configureOptions);
        }

        /// <summary>
        /// 后配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="configureOptions">选项操作</param>
        protected void PostConfigureAll<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            ServiceConfigurationContext.Services.PostConfigureAll(configureOptions);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 判断指定类型是否<see cref="IBingModule"/>类型
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsBingModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return typeInfo.IsClass &&
                   !typeInfo.IsAbstract &&
                   !typeInfo.IsGenericType &&
                   typeof(IBingModule).GetTypeInfo().IsAssignableFrom(type);
        }

        /// <summary>
        /// 检查模块类型是否<see cref="IBingModule"/>类型
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        internal static void CheckBingModuleType(Type moduleType)
        {
            if (!IsBingModule(moduleType))
                throw new ArgumentException("Given type is not an Bing Module: " + moduleType.AssemblyQualifiedName);
        }

        #endregion

    }
}
