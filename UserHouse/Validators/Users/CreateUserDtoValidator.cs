using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using UserHouse.Web.Host.Dtos.Users;

namespace UserHouse.Web.Host.Validators.Users
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().OnFailure(
                    x => throw new Exception(
                        "First name is required!"))
                .MaximumLength(64).OnFailure(
                    x => throw new Exception(
                        "Max length is 64 symbols!"));

            RuleFor(x => x.LastName)
                .NotEmpty().OnFailure(
                    x => throw new Exception(
                        "Last name is required!"))
                .MaximumLength(64).OnFailure(
                    x => throw new Exception(
                        "Max length is 64 symbols!"));
        }
    }
}
