using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IS.Core.Constants;
using ISConsoleApp.Options;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace ISConsoleApp
{
    public class TableService : BackgroundService
    {
        private readonly ILogger<TableService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public TableService(
            ILogger<TableService> logger,
            IServiceScopeFactory scopeFactory,
            IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _applicationLifetime = applicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Starting at {DateTime.UtcNow}. INFORMATION logged from Assembly '{AssemblyName.ISConsoleApp}'");
            _logger.LogInformation($"-------------------------------------------------------------------------------------------");
            _logger.LogInformation($"Requested Drawing");
            _logger.LogInformation($"-------------------------------------------------------------------------------------------");

            using (var scope = _scopeFactory.CreateScope())
            {
                var start = scope.ServiceProvider.GetRequiredService<Start>();

                try
                {
                    await Task.Run(() => start.TableOptions(new TableOptions()));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Drawer Service exception. ERROR logged from Assembly '{AssemblyName.ISConsoleApp}'");
                    _applicationLifetime.StopApplication();
                }
            }

            _logger.LogInformation($"Stopped Drawer Service at {DateTime.UtcNow}. INFORMATION logged from Assembly '{AssemblyName.ISConsoleApp}'");
            //Stop Application as 
            _applicationLifetime.StopApplication();
        }
    }
}