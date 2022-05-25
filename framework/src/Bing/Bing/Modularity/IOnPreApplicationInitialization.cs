using System.Threading.Tasks;

namespace Bing.Modularity
{
    /// <summary>
    /// 应用程序启动前
    /// </summary>
    public interface IOnPreApplicationInitialization
    {
        /// <summary>
        /// 应用程序启动前
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法在 OnApplicationInitialization 方法执行之前被调用。
        /// 在这个阶段，可以从依赖注入中解析服务，因为服务已经被初始化。
        /// </remarks>
        Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context);

        /// <summary>
        /// 应用程序启动前
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法在 OnApplicationInitialization 方法执行之前被调用。
        /// 在这个阶段，可以从依赖注入中解析服务，因为服务已经被初始化。
        /// </remarks>
        void OnPreApplicationInitialization(ApplicationInitializationContext context);
    }
}
