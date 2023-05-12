using aw2_ozturkdogukan.Data.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aw2_ozturkdogukan.Data.FluentValidation
{

    public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Phone).NotNull().NotEmpty();
            RuleFor(x => x.City).NotNull().NotEmpty();
            RuleFor(x => x.Country).NotNull().NotEmpty();
            RuleFor(x => x.Province).NotNull().NotEmpty();
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty();
            RuleFor(x => x.AddressLine1).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
        }

    }
}
