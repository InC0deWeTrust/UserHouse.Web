using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using UserHouse.Application.Dtos.Users;
using UserHouse.Application.Helpers;

namespace UserHouse.Application.Validators.Users
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "First name is required!"))
                .MaximumLength(64).OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Max length is 64 symbols!"));

            RuleFor(x => x.LastName)
                .NotEmpty().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Last name is required!"))
                .MaximumLength(64).OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Max length is 64 symbols!"));

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Date of Birth is required!"));

            RuleFor(x => x.Email)
                .EmailAddress().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Email is required!"))
                .MaximumLength(64).OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Max length is 64 symbols!"));
        }
    }
}
