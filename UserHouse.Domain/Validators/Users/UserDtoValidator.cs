using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using UserHouse.Application.Dtos.Users;
using UserHouse.Application.Helpers;

namespace UserHouse.Application.Validators.Users
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
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

            RuleFor(x => x.UserId)
                .NotEmpty().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "UserId is required!"));

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
