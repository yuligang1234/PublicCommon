using System;
using System.Web;
using System.Web.Caching;

namespace Napoleon.PublicCommon.Http
{
    public static class CacheFunc
    {

        /// <summary>
        ///  写入缓存(相对过期)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="dep">缓存依赖项</param>
        /// <param name="timespan">相对过期时间(1小时)</param>
        /// <param name="priority">该对象相对于缓存中存储的其他项的成本，由 CacheItemPriority 枚举表示。该值由缓存在退出对象时使用；具有较低成本的对象在具有较高成本的对象之前被从缓存移除。</param>
        /// Author  : Napoleon
        /// Created : 2016-02-14 11:30:58
        public static void InsertSlidingCache(this string key, object value, CacheDependency dep = null, double timespan = 1, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            HttpRuntime.Cache.Insert(key, value, dep, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(timespan), priority, null);
        }

        /// <summary>
        ///  写入缓存(绝对过期)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="dep">缓存依赖项</param>
        /// <param name="datetime">绝对过期时间(1小时)</param>
        /// <param name="priority">该对象相对于缓存中存储的其他项的成本，由 CacheItemPriority 枚举表示。该值由缓存在退出对象时使用；具有较低成本的对象在具有较高成本的对象之前被从缓存移除。</param>
        /// Author  : Napoleon
        /// Created : 2016-02-14 11:30:58
        public static void InsertAbsoluteCache(this string key, object value, CacheDependency dep = null, double datetime = 1,
            CacheItemPriority priority = CacheItemPriority.Normal)
        {
            HttpRuntime.Cache.Insert(key, value, dep, DateTime.Now.AddHours(datetime), Cache.NoSlidingExpiration, priority, null);
        }

        /// <summary>
        ///  获取缓存的通用类
        /// </summary>
        /// <param name="key">key</param>
        /// Author  : Napoleon
        /// Created : 2016-02-14 13:47:49
        private static object GetCache(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        /// <summary>
        ///  获取字符串缓存
        /// </summary>
        /// <param name="key">The key.</param>
        /// Author  : Napoleon
        /// Created : 2016-02-14 14:22:01
        public static string GetStringCache(this string key)
        {
            var cache = GetCache(key);
            return cache != null ? cache.ToString() : "";
        }

        /// <summary>
        ///  获取实体类缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// Author  : Napoleon
        /// Created : 2016-02-14 14:03:21
        public static T GetListCache<T>(this string key) where T : class
        {
            return GetCache(key) as T;
        }

        /// <summary>
        ///  清除单个缓存
        /// </summary>
        /// <param name="key">key</param>
        /// Author  : Napoleon
        /// Created : 2016-02-14 11:32:44
        public static void RemoveCache(this string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

    }
}
