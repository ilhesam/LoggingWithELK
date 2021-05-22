using System;
using Serilog;
using Serilog.Configuration;

namespace BuyService.Logging
{
    public static class LoggingExtensions
    {
        public static LoggerConfiguration CustomerLogEnricher(this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null) throw new ArgumentNullException(nameof(enrich));
            return enrich.With<CustomLogEnricher>();
        }
    }
}