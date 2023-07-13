using OrderEvaluationFunction;
using System;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OrderEvaluationFunction.Consumers;

[assembly: FunctionsStartup(typeof(Startup))]

namespace OrderEvaluationFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddMassTransit(c =>
                {
                    c.AddConsumersFromNamespaceContaining<ConsumerNamespace>();
                    c.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                })
                .AddSingleton<IAsyncBusHandle, AsyncBusHandle>()
                .RemoveMassTransitHostedService();

            //.AddMassTransitForAzureFunctions(cfg =>
            //    {
            //        cfg.AddConsumersFromNamespaceContaining<ConsumerNamespace>();
            //    },
            //    "AzureWebJobsServiceBus",
            //    (ctx, cfg) =>
            //    {
            //        cfg.ConfigureEndpoints(ctx);
            //    });

        }
    }
}
