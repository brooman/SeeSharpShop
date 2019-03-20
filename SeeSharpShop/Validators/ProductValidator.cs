using System;
using FluentValidation;
using SeeSharpShop.Models;

namespace SeeSharpShop.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).Length(2, 64).NotEmpty().WithMessage("Invalid product name");
            RuleFor(x => x.Description).Length(0, 256).WithMessage("Description cannot be longer than 256 characters");
            RuleFor(x => x.Cost).NotEmpty().WithMessage("Please specify a cost");
        }
    }
}
