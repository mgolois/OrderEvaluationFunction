using System;
using MassTransit;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using OrderEvaluationFunction.Messages;

namespace OrderEvaluationFunction.Functions
{
    public class OrerTimer
    {
        private readonly ILogger<OrerTimer> _logger;
        private readonly IPublishEndpoint _publisher;
        IAsyncBusHandle _bus;
        public OrerTimer(IPublishEndpoint publisher, ILogger<OrerTimer> logger, IAsyncBusHandle bus)
        {
            _publisher = publisher;
            _logger = logger;
            _bus = bus;
        }

        //Every 15seconds
        [FunctionName("OrderTimer")]
        public void Run([TimerTrigger("*/60 * * * * *")] TimerInfo timerInfo, ILogger log)
        {
            log.LogInformation($"OrderTimer executed at: {DateTime.Now}");

            //Get all expired orders, and publish each one

            _publisher.Publish<OrderExpired>(new
            {
                OrderId = Guid.NewGuid(),
                DateTime = DateTime.Now
            });
        }
    }
}
