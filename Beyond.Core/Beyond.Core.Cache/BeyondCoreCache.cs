using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Created by AllenLee at 2017.10.24 */

namespace Beyond.Core.Cache
{
    public class BeyondCoreCache
    {
        private static object _locker = new object();//锁对象
        private static ICache _icache = null;//缓存策略
        static BeyondCoreCache()
        {
            if (_icache == null)
            {
                lock (_locker)
                {
                    if (_icache == null)
                    {
                        CacheInit();
                    }
                }
            }
        }

        private static void CacheInit()
        {
            //E:\VSApplication\Project\Project.Azure.App\WinMemCache\Config\Web.config
            //获取配置文件，是启用哪种缓存
            string config = ConfigurationManager.AppSettings["CacheType"] ?? "";
            string message = "";
            if (config.ToLower() == "memcached")
            {
                try
                {
                    string[] servers = ConfigurationManager.AppSettings["MemCacheServers"].Split(',');
                    _icache = new MemCache(servers, config);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
            else if (config.ToLower() == "rediscached")
            {
                try
                {

                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
            else
            {
                try
                {
                    _icache = new NETCache();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
        }

        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        public static T GetCache<T>(string key)
        {
            return string.IsNullOrEmpty(key) ? default(T) : _icache.GetCache<T>(key);
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中，默认缓存1小时
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        public static void SetCache<T>(string key, T data)
        {
            if (string.IsNullOrEmpty(key) || data == null)
                return;
            lock (_locker)
            {
                _icache.SetCache<T>(key, data);
            }
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        public static void SetCache<T>(string key, T data, DateTime time)
        {
            if (string.IsNullOrEmpty(key) || data == null)
                return;
            lock (_locker)
            {
                _icache.SetCache<T>(key, data, time);
            }
        }

        /// <summary>
        /// 从缓存中移除指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        public static void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            lock (_locker)
            {
                _icache.DeleteCache(key);
            }
        }

        /// <summary>
        /// 清空缓存所有对象
        /// </summary>
        public static void FlushAll()
        {
            lock (_locker)
            {
                _icache.FlushAll();
            }
        }

        public static void DeleteSynchro(string key)
        {
            Action<string> Do = new Action<string>(Delete);
            Do.BeginInvoke(key, null, null);
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        public static void SetSynchro<T>(string key, T data, DateTime time)
        {
            Action<string, T, DateTime> Do = new Action<string, T, DateTime>(SetCache);
            Do.BeginInvoke(key, data, time, null, null);
        }


        /// <summary>
        /// 获取所有已注册CacheKey
        /// </summary>
        /// <returns></returns>
        //public static List<KeyAttribute> GetAllCacheKeys()
        //{

        //    List<KeyAttribute> list = new List<KeyAttribute>();


        //    foreach (FieldInfo fi in typeof(CacheKeys).GetFields(BindingFlags.Static
        //        | BindingFlags.Public | BindingFlags.NonPublic))
        //    {
        //        var v = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //        var value = fi.GetValue(null).ToString();
        //        var description = v == null ? fi.Name : v[0].Description;
        //        KeyAttribute attr = new KeyAttribute() { Name = fi.Name, Key = value, Description = description };
        //        list.Add(attr);
        //    }

        //    return list;
        //}
    }
}

