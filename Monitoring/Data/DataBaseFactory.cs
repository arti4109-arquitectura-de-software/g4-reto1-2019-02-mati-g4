using Autofac;
using Monitoring.Data.Redis;
using Monitoring.Data.Redis.Config;


namespace Monitoring.Data
{
    public static class DataBaseFactory
    {
         public static void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigRedis>().As<IConfigRedis>();
            builder.RegisterType<RedisCacheProvider>().As<ICacheProvider>();
        }
    }
}
