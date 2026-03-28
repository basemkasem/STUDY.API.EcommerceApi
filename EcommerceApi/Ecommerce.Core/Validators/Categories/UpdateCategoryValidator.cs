using Ecommerce.Core.DTOs.Category;
using FluentValidation;

namespace Ecommerce.Core.Validators.Categories;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .MaximumLength(50)
            .WithMessage("Category name cannot exceed 50 characters.");
    }
}