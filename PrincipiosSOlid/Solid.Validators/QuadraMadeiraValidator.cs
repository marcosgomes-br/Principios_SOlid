
using FluentValidation;
using Solid.Domain;

namespace Solid.Validators
{
    public class QuadraMadeiraValidator : AbstractValidator<QuadraMadeiraDomain>
    {
        public QuadraMadeiraValidator()
        {
            RuleFor(x => x.QuantidadeMadeiraSolta).Equal(0).WithMessage("A quadra precisa de reparo");
        }
    }
}
