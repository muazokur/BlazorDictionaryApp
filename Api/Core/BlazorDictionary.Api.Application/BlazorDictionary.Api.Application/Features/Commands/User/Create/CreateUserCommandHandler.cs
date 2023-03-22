using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common;
using BlazorDictionary.Common.Event.User;
using BlazorDictionary.Common.Infrastructure;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existUser=await userRepository.GetSingleAsync(i=>i.EmailAdress==request.EmailAdress);

            if (existUser is not null)
                throw new DatabaseValidationException("User already exist!");

            request.Password=PasswordEncryptor.Encrypt(request.Password);

            var dbUser = mapper.Map<Domain.Models.User>(request);

            var rows = await userRepository.AddAsync(dbUser);

            //Email changed or Email Created

            if (rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAdress = null,
                    NewEmailAdress = request.EmailAdress,

                };

                QueueFactory.SendMessageToExchange(exchangeName : DictionaryConstants.UserExcangeName, exchangeType : DictionaryConstants.DefaultExchangeType, queueName : DictionaryConstants.UserEmailExcangedQueueName, obj: @event);
            }

            return dbUser.Id;
        }
    }
}
