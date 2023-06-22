using System;
using MassTransit;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using OrderEvaluationFunction.Messages;

namespace OrderEvaluationFunction
{
    public class OrerTimer
    {
        private readonly ILogger<OrerTimer> _logger;
        private readonly IPublishEndpoint _publisher;
        public OrerTimer(IPublishEndpoint publisher, ILogger<OrerTimer> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        //Every 15seconds
        [FunctionName("OrderTimer")]
        public void Run([TimerTrigger("*/15 * * * * *")]TimerInfo timerInfo, ILogger log)
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
