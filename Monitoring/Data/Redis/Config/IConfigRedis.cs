
namespace Monitoring.Data.Redis.Config
{
    public interface IConfigRedis
    {
        string Host { get;}
        int Port { get; }
        string Password { get; }
        long DatabaseId { get; }

    }
}
