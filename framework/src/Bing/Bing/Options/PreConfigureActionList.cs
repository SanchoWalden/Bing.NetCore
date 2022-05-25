using System;
using System.Collections.Generic;

namespace Bing.Options
{
    /// <summary>
    /// 预先配置操作列表
    /// </summary>
    /// <typeparam name="TOptions">配置选项类型</typeparam>
    public class PreConfigureActionList<TOptions> : List<Action<TOptions>>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="options">选项配置</param>
        public void Configure(TOptions options)
        {
            foreach (var action in this) 
                action(options);
        }

        /// <summary>
        /// 配置
        /// </summary>
        public TOptions Configure()
        {
            var options = Activator.CreateInstance<TOptions>();
            Configure(options);
            return options;
        }
    }
}
