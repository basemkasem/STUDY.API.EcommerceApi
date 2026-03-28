using Ecommerce.Core.DTOs.Category;
using FluentValidation;

namespace Ecommerce.Core.Validators.Categories;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .MaximumLength(50)
            .WithMessage("Category name cannot exceed 50 characters.");
    }
}