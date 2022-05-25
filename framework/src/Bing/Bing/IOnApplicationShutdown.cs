using System.Threading.Tasks;

namespace Bing
{
    /// <summary>
    /// 应用程序关闭
    /// </summary>
    public interface IOnApplicationShutdown
    {
        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context">应用关闭上下文</param>
        /// <remarks>
        /// 根据需要自己实现模块的关闭逻辑。
        /// </remarks>
        Task OnApplicationShutdownAsync(ApplicationShutdownContext context);

        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context">应用关闭上下文</param>
        /// <remarks>
        /// 根据需要自己实现模块的关闭逻辑。
        /// </remarks>
        void OnApplicationShutdown(ApplicationShutdownContext context);
    }
}
