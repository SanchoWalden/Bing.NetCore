using System;
using System.Collections.Generic;
using Bing.Collections;

namespace Bing.Logging
{
    /// <summary>
    /// 默认的初始化日志工厂
    /// </summary>
    public class DefaultInitLoggerFactory : IInitLoggerFactory
    {
        /// <summary>
        /// 缓存字典
        /// </summary>
        private readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();

        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public virtual IInitLogger<T> Create<T>()
        {
            return (IInitLogger<T>)_cache.GetOrAdd(typeof(T), () => new DefaultInitLogger<T>());
        }
    }
}
