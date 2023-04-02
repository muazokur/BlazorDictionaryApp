using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BlazorDictionary.Projections.FavoriteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connStr = configuration.GetConnectionString("SqlServer");

            var favService = new Services.FavoriteService(connStr);

            QueueFactory.CreateBasicConsumer()
                 .EnsureExchange(DictionaryConstants.FavExcahangeName)
                 .EnsureQueue(DictionaryConstants.CreateEntryFavQueueName, DictionaryConstants.FavExcahangeName)
                 .Receive<CreateEntryFavEvent>(fav =>
                 {
                     favService.CreateEntryFav(fav).GetAwaiter().GetResult();
                     _logger.LogInformation($"Received EntryId {fav.EntryId}");
                 }).StartConsuming(DictionaryConstants.CreateEntryFavQueueName);
        }
    }
}