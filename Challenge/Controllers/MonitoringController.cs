
using Challenge.Model;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Data.Redis;
using RabbitMQ.Client.Core.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {

        private readonly ICacheProvider _cacheProvider;
        private readonly IQueueService _queueService;

        public MonitoringController(ICacheProvider cacheProvider, IQueueService queueService)
        {
            _cacheProvider = cacheProvider;
            _queueService = queueService;
        }

        [HttpPost]
        public async Task<bool> SetMessage (Monitor monitor)
        {
            bool result = false;

            try
            {
                Guid guid = Guid.NewGuid();

                monitor.Id = $"car_{guid}";

                await Task.Run(() => {
                    _cacheProvider.Set(guid.ToString(), monitor);
                    _queueService.Send(
                                    @object: monitor,
                                    exchangeName: "amq.direct",
                                    routingKey: "general");
                    });

                result = true;
            }
            catch
            {
                
            }

            return result ;
        }

        [HttpGet]
        public async Task<Monitor> GetMessage(Guid id)
        {
            Monitor result = null;

            try
            {
                string key = $"car_{id}";

                result = await Task.Run(() => _cacheProvider.Get<Monitor>(id.ToString()));
            }
            catch
            {

            }

            return result;
        }
    }
}