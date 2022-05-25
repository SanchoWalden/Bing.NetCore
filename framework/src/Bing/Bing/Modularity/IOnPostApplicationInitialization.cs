using System.Threading.Tasks;

namespace Bing.Modularity
{
    /// <summary>
    /// 应用程序启动后
    /// </summary>
    public interface IOnPostApplicationInitialization
    {
        /// <summary>
        /// 应用程序启动后
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法在 OnApplicationInitialization 方法执行之后被调用。
        /// </remarks>
        Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context);

        /// <summary>
        /// 应用程序启动后
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法在 OnApplicationInitialization 方法执行之后被调用。
        /// </remarks>
        void OnPostApplicationInitialization(ApplicationInitializationContext context);
    }
}
