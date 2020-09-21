using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Inlamningsuppgift_1
{
    public class Worker : BackgroundService
    {

        private readonly ILogger<Worker> _logger;
        private readonly Random _random = new Random();
                
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has now started");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("The service has now stopped");
            return base.StopAsync(cancellationToken);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    int _temp = _random.Next(-30, 25);
                    if (_temp < -15)
                    {
                        _logger.LogInformation($"Warning! Stay indoors! The temperature is: {_temp} °C and it's too cold.");
                    }
                  
                    else
                    {
                        _logger.LogInformation($"It is a good day to be outside. Dress accordingly. The temperature is {_temp} °C!");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Service failed. The temperature could not be logged. - {ex.Message}");
                }

                await Task.Delay(60 * 1000, stoppingToken);
            }
        }
    }
}