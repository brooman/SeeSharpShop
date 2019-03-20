using System;
using FluentValidation;
using SeeSharpShop.Models;
using SeeSharpShop.Repositories;

namespace SeeSharpShop.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        private readonly ProductRepository repository;
        public ProductValidator(ProductRepository repository)
        {
            this.repository = repository;

            RuleFor(x => x.Name).Length(2, 64).NotEmpty().WithMessage("Invalid product name");
            RuleFor(x => x.Description).Length(0, 256).WithMessage("Description cannot be longer than 256 characters");
            RuleFor(x => x.Cost).NotEmpty().WithMessage("Please specify a cost");

            RuleSet("MustExist", () => {
                RuleFor(x => x.Id).Must(Exist).WithMessage("Requested product doesn't exist");
            });
        }

        protected bool Exist(int id)
        {
           return repository.Exists(id);
        }
    }
}
