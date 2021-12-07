using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using UserHouse.Application.Dtos.Permissions;
using UserHouse.Application.Helpers;

namespace UserHouse.Application.Validators.Permissions
{
    public class PermissionDtoValidator : AbstractValidator<PermissionDto>
    {
        public PermissionDtoValidator()
        {
            RuleFor(x => x.PermissionName)
                .NotEmpty().OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Permission name is required!"))
                .MaximumLength(64).OnFailure(
                    x => throw new CustomUserFriendlyException(
                        "Max length is 64 symbols!"));
        }
    }
}
