using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.dto;

namespace UserServices.Validators
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(e => e.Name).MinimumLength(10).MaximumLength(200).WithMessage("must have more than 10 and less than 200 symbols");
        }
    }
}
