using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Created by AllenLee at 2017.10.24 */

namespace Beyond.Core.Cache
{
    public class MemCache : ICache
    {
        private MemcachedClient _cached;
        public MemCache(string[] serverlist, string poolName)
        {
            //套接连接池，创建实例
            SockIOPool pool = SockIOPool.GetInstance(poolName);

            pool.SetServers(serverlist);
            pool.InitConnections = 1;
            pool.MinConnections = 1;
            pool.MaxConnections = 500;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();//容器初始化

            _cached = new MemcachedClient { PoolName = poolName, EnableCompression = false };
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
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        bool ICache.AddCache<T>(string key, T value)
        {
            return _cached.Add(key, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireTime">缓存过期时间</param>
        /// <returns></returns>
        bool ICache.AddCache<T>(string key, T value, DateTime expireTime)
        {
            return _cached.Add(key, value, expireTime);
        }
        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        bool ICache.DeleteCache(string key)
        {
            return _cached.Delete(key);
        }
        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="expireTime">缓存过期时间</param>
        /// <returns></returns>
        bool ICache.DeleteCache(string key, DateTime expireTime)
        {
            return _cached.Delete(key, expireTime);
        }
        /// <summary>
        /// 设置指定缓存值
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        bool ICache.SetCache<T>(string key, T value)
        {
            return _cached.Set(key, value);
        }
        /// <summary>
        /// 设置指定缓存值
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireTime">缓存过期时间</param>
        /// <returns></returns>
        bool ICache.SetCache<T>(string key, T value, DateTime expireTime)
        {
            return _cached.Set(key, value, expireTime);
        }
        /// <summary>
        /// 查询缓存值
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        T ICache.GetCache<T>(string key)
        {
            return (T)_cached.Get(key);
        }
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        /// <returns></returns>
        public bool FlushAll()
        {
            return _cached.FlushAll();
        }
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return _cached.KeyExists(key);
        }
    }
}
