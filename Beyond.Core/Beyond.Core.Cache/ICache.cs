using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Created by AllenLee at 2017.10.24 */

namespace Beyond.Core.Cache
{
    public partial interface ICache
    {
        /// <summary>
        /// 默认过期时间(秒
        /// </summary>
        int TimeOut { get; set; }
        /// <summary>
        /// 添加新缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        bool AddCache<T>(string key, T value);
        /// <summary>
        /// 添加新缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireTime">缓存时间</param>
        /// <returns></returns>
        bool AddCache<T>(string key, T value, DateTime expireTime);
        /// <summary>
        /// 设置、修改缓存值
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <returns></returns>
        bool SetCache<T>(string key, T value);
        /// <summary>
        /// 设置、修改缓存值
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="expireTime">缓存时间</param>
        /// <returns></returns>
        bool SetCache<T>(string key, T value, DateTime expireTime);
        /// <summary>
        /// 获取指定键的缓存值
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        T GetCache<T>(string key);
        /// <summary>
        /// 删除指定键缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        bool DeleteCache(string key);
        /// <summary>
        /// 删除指定键缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="expireTime">缓存过期时间</param>
        /// <returns></returns>
        bool DeleteCache(string key, DateTime expireTime);
        /// <summary>
        /// 清空所有缓存
        /// </summary>
        /// <returns></returns>
        bool FlushAll();
        /// <summary>
        /// 判断指定缓存键值是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KeyExists(string key);
    }
}
