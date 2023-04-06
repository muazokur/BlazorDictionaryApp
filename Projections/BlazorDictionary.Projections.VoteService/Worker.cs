using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Infrastructure.Extensions;
using BlazorDictionary.Common;
using Microsoft.Extensions.Configuration;

namespace BlazorDictionary.Projections.VoteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration configuration;

        public Worker(ILogger<Worker> logger,IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           // var connStr = configuration.GetConnectionString("SqlServer");
            var connStr = "Server=DESKTOP-G7KHF4G\\SQLEXPRESS;Initial Catalog=BlazorDictionaryDB;Integrated security=true";
            var voteService = new Services.VoteService(connStr);


            QueueFactory.CreateBasicConsumer()
                 .EnsureExchange(DictionaryConstants.VoteExchangeName)
                 .EnsureQueue(DictionaryConstants.CreateEntryVoteQueueName, DictionaryConstants.VoteExchangeName)
                 .Receive<CreateEntryVoteEvent>(vote =>
                 {
                     voteService.CreateEntryVote(vote).GetAwaiter().GetResult();

                     _logger.LogInformation($"Create Entry Received EntryId: {0}, VoteType: {1} ",vote.EntryId,vote.VoteType);

                 }).StartConsuming(DictionaryConstants.CreateEntryVoteQueueName);
        }
    }
}