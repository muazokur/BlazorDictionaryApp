using BlazorDictionary.Common;
using BlazorDictionary.Common.Event.EntryComment;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Events.EntryComment;
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

            QueueFactory.CreateBasicConsumer()
               .EnsureExchange(DictionaryConstants.FavExcahangeName)
               .EnsureQueue(DictionaryConstants.DeleteEntryFavQueueName, DictionaryConstants.FavExcahangeName)
               .Receive<DeleteEntryFavEvent>(fav =>
               {
                   favService.DeleteEntryFav(fav).GetAwaiter().GetResult();
                   _logger.LogInformation($"Deleted Received EntryId {fav.EntryId}");
               }).StartConsuming(DictionaryConstants.DeleteEntryFavQueueName);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(DictionaryConstants.FavExcahangeName)
                .EnsureQueue(DictionaryConstants.CreateEntryCommentFavQueueName, DictionaryConstants.FavExcahangeName)
                .Receive<CreateEntryCommentFavEvent>(fav =>
                {
                    favService.CreateEntryCommentFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Created EntryComment Received EntryCommentId {fav.EntryCommentId}");
                }).StartConsuming(DictionaryConstants.CreateEntryCommentFavQueueName);

            QueueFactory.CreateBasicConsumer()
             .EnsureExchange(DictionaryConstants.FavExcahangeName)
             .EnsureQueue(DictionaryConstants.DeleteEntryCommentFavQueueName, DictionaryConstants.FavExcahangeName)
             .Receive<DeleteEntryCommentFavEvent>(fav =>
             {
                 favService.DeleteEntryCommentFav(fav).GetAwaiter().GetResult();
                 _logger.LogInformation($"Deleted Received EntryId {fav.EntryCommentId}");
             }).StartConsuming(DictionaryConstants.DeleteEntryCommentFavQueueName);
        }
    }
}