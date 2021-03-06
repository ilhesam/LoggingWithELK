using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;

namespace BuyService.Logging
{
    public interface ICustomLogEnricher
    {
        string CorrelationId { get; set; }
    }

    public class CustomLogEnricher : ILogEventEnricher, ICustomLogEnricher
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public string CorrelationId { get; set; }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (ServiceProvider?.GetRequiredService<IHttpContextAccessor>().HttpContext is { } httpContext)
            {
                var headers = httpContext.Request.Headers;
                CorrelationId = headers["CorrelationId"];

                logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId", new ScalarValue(CorrelationId)));
            }
        }
    }
}