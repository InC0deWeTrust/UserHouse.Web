using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using UserHouse.Application.Dtos.Login;
using UserHouse.Application.Helpers;

namespace UserHouse.Application.Validators.Login
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "First name is required!"))
                .MaximumLength(64).OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Max length is 64 symbols!"));

            RuleFor(x => x.Email)
                .EmailAddress().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "This should be an email!"))
                .MaximumLength(64).OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Max length is 64 symbols!"));
        }
    }
}
