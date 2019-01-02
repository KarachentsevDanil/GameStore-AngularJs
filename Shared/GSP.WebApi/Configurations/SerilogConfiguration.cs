using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;

namespace GSP.WebApi.Configurations
{
    public static class SerilogConfiguration
    {
        public static void Apply(LoggerConfiguration logger, IConfiguration config)
        {
            logger
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .ReadFrom.Configuration(config);
        }
    }
}