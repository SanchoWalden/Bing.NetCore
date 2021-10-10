using System;
using System.Collections.Generic;

namespace Bing.Logs.ExtraSupports
{
    /// <summary>
    /// 日志事件上下文数据异常详情
    /// </summary>
    public class ContextDataExceptionDetail : ContextDataItem
    {
        /// <summary>
        /// 初始化一个<see cref="ContextDataItem"/>类型的实例
        /// </summary>
        /// <param name="rootName">根名称</param>
        /// <param name="destructuredObject">异常已解构对象</param>
        /// <param name="exception">异常</param>
        /// <param name="output">是否输出</param>
        public ContextDataExceptionDetail(string rootName, IReadOnlyDictionary<string, object> destructuredObject, Exception exception, bool output) : base(rootName, exception.GetType(), destructuredObject, output)
        {
        }

        /// <summary>
        /// 获取异常详情
        /// </summary>
        /// <returns></returns>
        public IReadOnlyDictionary<string, object> GetExceptionDetail() => Value as IReadOnlyDictionary<string, object>;

        // (string, IReadOnlyDictionary<string, object>) item
        public static implicit operator (string, IReadOnlyDictionary<string, object>)(ContextDataExceptionDetail item) => (item.Name, item.GetExceptionDetail());
    }
}
