using System.Threading.Tasks;

namespace Bing
{
    /// <summary>
    /// 应用程序启动
    /// </summary>
    public interface IOnApplicationInitialization
    {
        /// <summary>
        /// 应用程序启动
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法用来配置 ASP.NET Core 请求管道并初始化你的服务。
        /// </remarks>
        Task OnApplicationInitializationAsync(ApplicationInitializationContext context);

        /// <summary>
        /// 应用程序启动
        /// </summary>
        /// <param name="context">应用程序初始化上下文</param>
        /// <remarks>
        /// 该方法用来配置 ASP.NET Core 请求管道并初始化你的服务。
        /// </remarks>
        void OnApplicationInitialization(ApplicationInitializationContext context);
    }
}
