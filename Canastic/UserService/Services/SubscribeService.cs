using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlayerService.Controllers;
using SharedService.Models;

namespace PlayerService.Services
{
    public class SubscribeService : BackgroundService
    {
        private const string DbTable = "Players";
        public readonly IBus _bus;
        
        public readonly MongoService DbService;
        public readonly PlayerController controller;
        private readonly ILogger _logger;


        public SubscribeService(ILogger<SubscribeService> logger)
        {
            _bus = RabbitHutch.CreateBus("host=rabbitmq;timeout=60");
            DbService = new(DbTable);
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            await _bus.PubSub.SubscribeAsync<GameFinishedDTO>("GameFinished", HandleMessage);
        }

        private async void HandleMessage(GameFinishedDTO obj)
        {
            _logger.LogInformation("Message received from rabbitMQ");
            foreach (var id in obj.winners)
            {
                var result = DbService.LoadRecordByID<PlayerDTO>(DbTable, id);
                result.TotalWins++;
                DbService.UpsertRecord(DbTable,result.Id,result);
            }
        }
    }
}
