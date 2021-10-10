using System.Collections.Generic;
using Bing.Logs.ExtraSupports;

namespace Bing.Logs
{
    /// <summary>
    /// 日志事件上下文
    /// </summary>
    public class LogEventContext
    {
        /// <summary>
        /// 日志上下文数据
        /// </summary>
        private readonly ContextData _contextData;

        /// <summary>
        /// 初始化一个<see cref="LogEventContext"/>类型的实例
        /// </summary>
        public LogEventContext()
        {
            _contextData = new ContextData();
        }

        /// <summary>
        /// 初始化一个<see cref="LogEventContext"/>类型的实例
        /// </summary>
        /// <param name="context">日志事件上下文数据</param>
        public LogEventContext(ContextData context)
        {
            _contextData = new ContextData(context);
        }

        /// <summary>
        /// 标签列表
        /// </summary>
        private readonly List<string> _tags = new List<string>();

        /// <summary>
        /// 标签列表
        /// </summary>
        public IReadOnlyList<string> Tags => _tags;

        /// <summary>
        /// 设置标签列表
        /// </summary>
        /// <param name="tags">标签列表</param>
        public LogEventContext SetTags(params string[] tags)
        {
            if (tags == null)
                return this;
            foreach (var tag in tags)
            {
                if(string.IsNullOrWhiteSpace(tag))
                    continue;
                if(_tags.Contains(tag))
                    continue;
                _tags.Add(tag);
            }
            return this;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        /// <param name="output">是否输出</param>
        public LogEventContext AddData(string name, object value, bool output = true)
        {
            _contextData.AddOrUpdateItem(name, value, output);
            return this;
        }

        /// <summary>
        /// 公开上下文数据
        /// </summary>
        public ContextData ExposeContextData() => _contextData;
    }
}
