using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace BlazorDictionary.Projections.UserService.Services
{
    public class EmailService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public string GenerateConfirmationLink(Guid confirmationId)
        {
            //var baseUrl = configuration["ConfirmationLinkBase"] + confirmationId; 
            var baseUrl = "https://localhost:7221/api/user/confirm?id=" + confirmationId;

            return baseUrl;
        }

        public Task SendEmailAddress(string toEmailAddress,string content)
        {
            //Send email process
            logger.LogInformation($"Email sent to {toEmailAddress} with content of {content}");

            return Task.CompletedTask;
        }
    }
}
