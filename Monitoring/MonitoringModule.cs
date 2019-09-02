using Autofac;
using Monitoring.Data;

namespace Monitoring
{
    public class MonitoringModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            DataBaseFactory.Load(builder);
        }
    }
}
