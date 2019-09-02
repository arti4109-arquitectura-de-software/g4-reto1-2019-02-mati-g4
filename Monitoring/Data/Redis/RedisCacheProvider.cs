using Monitoring.Data.Redis.Config;
using System;
using ServiceStack.Redis;

namespace Monitoring.Data.Redis
{
    public class RedisCacheProvider : RedisManagerPool, ICacheProvider
    {

        public RedisCacheProvider(IConfigRedis configRedis):
            base ($"{configRedis.Host}:{configRedis.Port}")
        {
     
        }

        public T Get<T>(string key)
        {
            T result = default;

            using (var client = GetClient())
            {
                var wrapper = client.As<T>();

                result = wrapper.GetValue(key);
            }

            return result;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (var client = GetClient())
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }

        public bool Remove(string key)
        {
            bool result = false;

            using (var client = GetClient())
            {
                result = client.Remove(key);
            }

            return result;
        }

        public void Set<T>(string key, T value)
        {
            using (var client = GetClient())
            {
                client.As<T>().SetValue(key, value);
            }
        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            using (var client = GetClient())
            {
                client.As<T>().SetValue(key, value,timeout);
            }
        }
    }
}
