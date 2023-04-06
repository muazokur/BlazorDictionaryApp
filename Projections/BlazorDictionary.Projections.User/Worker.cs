using BlazorDictionary.Common;
using BlazorDictionary.Common.Event.User;
using BlazorDictionary.Common.Infrastructure.Extensions;
using BlazorDictionary.Projections.UserService.Services;

namespace BlazorDictionary.Projections.User
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly UserService.Services.UserService userService;
        private readonly EmailService emailService;

        public Worker(ILogger<Worker> logger, UserService.Services.UserService userService, EmailService emailService)
        {
            _logger = logger;
            this.userService = userService;
            this.emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            QueueFactory.CreateBasicConsumer()
                 .EnsureExchange(DictionaryConstants.UserExcangeName)
                 .EnsureQueue(DictionaryConstants.UserEmailExcangedQueueName, DictionaryConstants.UserExcangeName)
                 .Receive<UserEmailChangedEvent>(user =>
                 {
                     //DB Insert
                     var confirmationId=userService.CreateEmailConfirmation(user).GetAwaiter().GetResult();
                     //Generate Link
                     var link=emailService.GenerateConfirmationLink(confirmationId);
                     //Send Email
                     emailService.SendEmailAddress(user.NewEmailAdress, link).GetAwaiter().GetResult();
                 }).StartConsuming(DictionaryConstants.UserEmailExcangedQueueName);

        }
    }
}