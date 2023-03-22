using AutoMapper;
using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailConfirmationRepository emailConfirmationRepository;
        private readonly IMapper mapper;

        public ConfirmEmailCommandHandler(IEmailConfirmationRepository emailConfirmationRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.emailConfirmationRepository = emailConfirmationRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmation = await emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

            if (confirmation is null)
                throw new DatabaseValidationException("Confirmation not found!");

            var dbUser = await userRepository.GetSingleAsync(i => i.EmailAdress == confirmation.NewEmailAdress);

            if(dbUser is null)
                throw new DatabaseValidationException("User not found with this Email!");

            if (dbUser.EmailConfirmend)
                throw new DatabaseValidationException("Email address is already confirmed!");

            dbUser.EmailConfirmend = true;
            await userRepository.UpdateAsync(dbUser);

            return true;


        }
    }
}
