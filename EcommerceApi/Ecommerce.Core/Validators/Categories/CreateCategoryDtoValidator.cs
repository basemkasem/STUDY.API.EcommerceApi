using Ecommerce.Core.DTOs.Category;
using FluentValidation;

namespace Ecommerce.Core.Validators.Categories;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .MaximumLength(50)
            .WithMessage("Category name cannot exceed 50 characters.");
    }
}