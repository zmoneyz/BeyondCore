using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
/* Created by AllenLee at 2017.10.24 */

namespace Beyond.Core.Cache
{
    public class NETCache : ICache
    {
        private System.Web.Caching.Cache _cache;

        public NETCache()
        {
            _cache = HttpRuntime.Cache;//运行时缓存
        }

        private int _timeout = 3600;//单位秒
        /// <summary>
        /// 缓存过期时间
        /// </summary>
        /// <value></value>
        public int TimeOut
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value > 0 ? value : 3600;
            }
        }
        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        public T GetCache<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        public bool AddCache<T>(string key, T value)
        {
            try
            {
                _cache.Insert(key, value, null, DateTime.Now.AddSeconds(this.TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool AddCache<T>(string key, T value, DateTime expiry)
        {
            try
            {
                _cache.Insert(key, value, null, expiry, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool SetCache<T>(string key, T value)
        {
            try
            {
                _cache.Insert(key, value, null, DateTime.Now.AddSeconds(this.TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool SetCache<T>(string key, T value, DateTime expiry)
        {
            try
            {
                _cache.Insert(key, value, null, expiry, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteCache(string key)
        {
            try
            {
                _cache.Remove(key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCache(string key, DateTime dt)
        {
            try
            {
                _cache.Remove(key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool FlushAll()
        {
            try
            {
                IDictionaryEnumerator cacheEnum = _cache.GetEnumerator();
                while (cacheEnum.MoveNext())
                {
                    _cache.Remove(cacheEnum.Key.ToString());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool KeyExists(string key)
        {
            return _cache.Get(key) != null;
        }
    }
}
