using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MassTransit;
using OrderEvaluationFunction.Messages;

namespace OrderEvaluationFunction.Functions
{
    public class PostOrder
    {
        readonly ILogger<PostOrder> _logger;
        readonly IPublishEndpoint _publisher;
        public PostOrder(ILogger<PostOrder> logger, IPublishEndpoint publisher)
        {
            _logger = logger;
            _publisher = publisher;
        }

        [FunctionName("PostOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("Order submitted.");
            var id = Guid.NewGuid();
            var order = new
            {
                OrderId = id,
                DateTime = DateTime.Now,
                Name = $"Order{id}",
            };

            await _publisher.Publish<OrderSubmitted>(order);

            return new OkObjectResult(order);
        }
    }
}
