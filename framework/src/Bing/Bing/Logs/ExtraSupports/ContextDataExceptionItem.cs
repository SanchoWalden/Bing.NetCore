using System;

namespace Bing.Logs.ExtraSupports
{
    /// <summary>
    /// 日志事件上下文数据异常项
    /// </summary>
    public class ContextDataExceptionItem : ContextDataItem
    {
        /// <summary>
        /// 初始化一个<see cref="ContextDataItem"/>类型的实例
        /// </summary>
        /// <param name="exception">异常</param>
        public ContextDataExceptionItem(Exception exception) : base(ContextDataTypes.Exception, exception.GetType(), exception)
        {
        }

        /// <summary>
        /// 获取异常
        /// </summary>
        public Exception GetException() => Value as Exception;

        // (Exception)item
        public static implicit operator Exception(ContextDataExceptionItem item) => item.GetException();
    }
}
