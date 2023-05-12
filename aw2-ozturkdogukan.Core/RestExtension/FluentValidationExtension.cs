using aw2_ozturkdogukan.Data.FluentValidation;
using aw2_ozturkdogukan.Data.Models;
using FluentValidation;

namespace aw2_ozturkdogukan.Core.RestExtension
{
    public static class FluentValidationExtension
    {
        public static void AddFluentValidationExtension(this IServiceCollection services)
        {
            services.AddScoped<AbstractValidator<Staff>, StaffValidator>();
            services.AddValidatorsFromAssemblyContaining<StaffValidator>();
        }
    }
}
