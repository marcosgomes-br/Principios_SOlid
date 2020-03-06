using FluentValidation;
using Solid.Domain;

namespace Solid.Validators
{
    public class QuadraGramaValidator : AbstractValidator<QuadraGramaDomain>
    {
        public QuadraGramaValidator()
        {
            RuleFor(x => x.AlturaGramaCm).Equal(x => x.AlturaIdealGrama).WithMessage("A grama precisa de cuidados...");
        }
    }
}
