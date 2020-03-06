using FluentValidation;
using Solid.Domain;

namespace Solid.Validators
{
    public class QuadraCimentoValidator : AbstractValidator<QuadraCimentoDomain>
    {
        public QuadraCimentoValidator()
        {
            RuleFor(x => x.QualidadePintura).Must(PinturaRuim).WithMessage("A quadra precisa de pintura");
        }

        private bool PinturaRuim(string status)
        {
            if(status.ToUpper() == "RUIM" || status.ToUpper() == "PÉSSIMO")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
