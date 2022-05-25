using System;
using System.Linq;
using Bing.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务集合 - 对象访问器 扩展
    /// </summary>
    public static class ServiceCollectionObjectAccessorExtensions
    {
        /// <summary>
        /// 尝试注册对象访问器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static ObjectAccessor<T> TryAddObjectAccessor<T>(this IServiceCollection services)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
                return services.GetSingletonInstance<ObjectAccessor<T>>();
            return services.AddObjectAccessor<T>();
        }

        /// <summary>
        /// 注册对象访问器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services)
        {
            return services.AddObjectAccessor(new ObjectAccessor<T>());
        }

        /// <summary>
        /// 注册对象访问器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="obj">对象</param>
        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, T obj)
        {
            return services.AddObjectAccessor(new ObjectAccessor<T>(obj));
        }

        /// <summary>
        /// 注册对象访问器
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="accessor">对象访问器</param>
        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, ObjectAccessor<T> accessor)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
                throw new Exception("An object accessor is registered before for type: " + typeof(T).AssemblyQualifiedName);

            // 插入到开头以便于快速检索
            services.Insert(0, ServiceDescriptor.Singleton(typeof(ObjectAccessor<T>), accessor));
            services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<T>), accessor));

            return accessor;
        }

        /// <summary>
        /// 获取可空对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        public static T GetObjectOrNull<T>(this IServiceCollection services)
            where T : class
        {
            return services.GetSingletonInstanceOrNull<IObjectAccessor<T>>()?.Value;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <exception cref="Exception"></exception>
        public static T GetObject<T>(this IServiceCollection services)
            where T:class
        {
            return services.GetObjectOrNull<T>() ?? throw new Exception($"Could not find an object of {typeof(T).AssemblyQualifiedName} in services. Be sure that you have used AddObjectAccessor before!");
        }
    }
}
