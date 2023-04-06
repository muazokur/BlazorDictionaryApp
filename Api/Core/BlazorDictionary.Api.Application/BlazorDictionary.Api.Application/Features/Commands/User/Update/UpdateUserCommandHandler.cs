using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Event.User;
using BlazorDictionary.Common;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using BlazorDictionary.Common.Models.RequestModels;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorDictionary.Common.Infrastructure.Extensions;

namespace BlazorDictionary.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);

            if(dbUser is null)
                throw new DatabaseValidationException("User not found");

            var dbEmailAddress = dbUser.EmailAdress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAdress) != 0;


            mapper.Map(request, dbUser);

            var rows = await userRepository.UpdateAsync(dbUser);


            //Check if email changed    

            if (rows > 0 && emailChanged)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAdress = dbEmailAddress,
                    NewEmailAdress = request.EmailAdress,

                };

                QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.UserExcangeName, exchangeType: DictionaryConstants.DefaultExchangeType, queueName: DictionaryConstants.UserEmailExcangedQueueName, obj: @event);

                dbUser.EmailConfirmend = false;
                await userRepository.UpdateAsync(dbUser);

            }
            return dbUser.Id;

        }
    }
}
