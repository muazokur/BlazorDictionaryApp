using BlazorDictionary.Common.Models.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(i => i.EmailAdress).NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address");

            RuleFor(i => i.UserName).NotNull().WithMessage("{PropertyName} cannot be null").MaximumLength(15).WithMessage("MaximumLengt: {MaxLengt}");
          
            RuleFor(i => i.FirstName).NotNull().WithMessage("{PropertyName} cannot be null").MaximumLength(20).WithMessage("MaximumLengt: {MaxLengt}");

            RuleFor(i => i.LastName).NotNull().WithMessage("{PropertyName} cannot be null").MaximumLength(20).WithMessage("MaximumLengt: {MaxLenght}");

            RuleFor(i => i.Password).NotNull()
                .MinimumLength(6).WithMessage("{PropertyName} should at least be {MinLenght} characters");
        }
    }
}
