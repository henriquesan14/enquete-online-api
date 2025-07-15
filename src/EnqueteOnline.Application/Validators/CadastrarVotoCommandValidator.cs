using EnqueteOnline.Application.Commands.CadastrarVoto;
using FluentValidation;

namespace EnqueteOnline.Application.Validators
{
    public class CadastrarVotoCommandValidator : AbstractValidator<CadastrarVotoCommand>
    {
        public CadastrarVotoCommandValidator()
        {
            RuleFor(d => d.EnqueteId)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("O campo {PropertyName} é obrigatório.");

            RuleFor(d => d.OpcaoEnqueteId)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("O campo {PropertyName} é obrigatório.");

        }
    }
}
