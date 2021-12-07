using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using UserHouse.Application.Dtos.Roles;
using UserHouse.Application.Helpers;

namespace UserHouse.Application.Validators.Roles
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Role name is required!"))
                .MaximumLength(64).OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Max length is 64 symbols!"));
        }
    }
}
