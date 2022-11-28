using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ISConsoleApp.Helpers;
using IS.Core.Constants;
using ISConsoleApp.Options;
using System;
using System.IO;

namespace ISConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            ILogger<Program> logger = null;
            try
            {
                // Create the host
                var host = CreateHostBuilder(args).Build();

                // Set up the logger in case get an exception
                logger = host.Services.GetRequiredService<ILogger<Program>>();
                logger.LogInformation($"Console App running. INFORMATION logged from Assembly '{AssemblyName.ISConsoleApp}'");

                // Start
                host.Run();
            }
            catch (OperationCanceledException)
            {
                if (logger != null)
                {
                    logger.LogWarning($"Console App operation cancelled. Application forced to shut down. WARNING logged from Assembly '{AssemblyName.ISConsoleApp}'");
                }
            }
            catch (Exception ex)
            {
                if (logger != null)
                {
                    logger.LogError(ex, $"Console App stopped due to an exception. ERROR logged from Assembly '{AssemblyName.ISConsoleApp}'");
                }

                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", false, true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddOptions();

                services.Configure<HostOptions>(option =>
                {
                    option.ShutdownTimeout = TimeSpan.FromSeconds(20);
                });

                services.AddServiceLayerServices();
                services.AddScoped<Start>();
                services.AddHostedService<TableService>();
                services.AddLogging(configure => configure.AddConsole());
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders(); // Clearing ALL default logging providers
                logging.AddConsole(); // Add Console provider
#if DEBUG
                logging.AddDebug(); // Add Debug provider when in Debug mode only
#endif
            });
    }
}