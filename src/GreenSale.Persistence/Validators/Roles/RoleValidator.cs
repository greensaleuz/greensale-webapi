using FluentValidation;
using FluentValidation.Validators;
using GreenSale.Persistence.Dtos.RoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Persistence.Validators.Roles
{
    public class RoleValidator:AbstractValidator<RoleCreatDto>
    {
        public RoleValidator()
        {
            RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
                .MinimumLength(3).WithMessage("Name must be more than 3 characters")
                        .MaximumLength(50).WithMessage("Name must be less than 50 characters");
        }
    }
}
