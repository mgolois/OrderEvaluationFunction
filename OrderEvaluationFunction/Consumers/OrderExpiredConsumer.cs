using MassTransit;
using Microsoft.Extensions.Logging;
using OrderEvaluationFunction.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEvaluationFunction.Consumers
{
    public class OrderExpiredConsumer: IConsumer<OrderExpired>
    {
        readonly ILogger<OrderExpiredConsumer> _logger;
        public OrderExpiredConsumer(ILogger<OrderExpiredConsumer> logger)
        {
           _logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderExpired> context)
         {
            await Task.Delay(30000);
            //TODO Email the user that the order has expired
            LogContext.Info?.Log ($"You order#{context.Message.OrderId} expired on {context.Message.DateTime}");
            await Task.CompletedTask;
        }
    }
}
