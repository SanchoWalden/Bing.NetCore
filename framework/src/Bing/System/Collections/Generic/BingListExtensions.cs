using Bing.Extensions;

namespace System.Collections.Generic
{
    /// <summary>
    /// 列表(<see cref="IList{T}"/>) 扩展
    /// </summary>
    public static class BingListExtensions
    {
        /// <summary>
        /// 移动项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="selector">选择器</param>
        /// <param name="targetIndex">目标索引</param>
        public static void MoveItem<T>(this List<T> source, Predicate<T> selector, int targetIndex)
        {
            targetIndex.CheckBetween(nameof(targetIndex), 0, source.Count - 1, true, true);

            var currentIndex = source.FindIndex(0, selector);
            if(currentIndex==targetIndex)
                return;
            var item = source[currentIndex];
            source.RemoveAt(currentIndex);
            source.Insert(targetIndex, item);
        }

        /// <summary>
        /// 按依赖项进行排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="getDependencies">获取依赖集合函数</param>
        /// <param name="comparer">依赖比较器</param>
        public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, IEqualityComparer<T> comparer = null)
        {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>(comparer);
            foreach (var item in source) 
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            return sorted;
        }

        /// <summary>
        /// 按依赖访问进行排序
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="item">数据项</param>
        /// <param name="getDependencies">获取依赖集合函数</param>
        /// <param name="sorted">排序后的列表</param>
        /// <param name="visited">已访问字典</param>
        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            var alreadyVisited = visited.TryGetValue(item, out var inProcess);
            if (alreadyVisited)
            {
                if (inProcess)
                    throw new ArgumentException("Cyclic dependency found! Item: " + item);
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                        SortByDependenciesVisit<T>(dependency, getDependencies, sorted, visited);
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}
