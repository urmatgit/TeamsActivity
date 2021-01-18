using AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Models;
using FluentValidation;

namespace AspNetCoreHero.Boilerplate.Web.Areas.Catalog.Validators
{
    public class InterestViewModelValidator : AbstractValidator<InterestViewModel>
    {
        public InterestViewModelValidator()
        {
          

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            
        }
    }
}