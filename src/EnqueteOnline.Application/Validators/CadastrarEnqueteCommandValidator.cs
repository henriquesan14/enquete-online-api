using EnqueteOnline.Application.Commands.CadastrarEnquete;
using FluentValidation;

namespace EnqueteOnline.Application.Validators
{
    public class CadastrarEnqueteCommandValidator : AbstractValidator<CadastrarEnqueteCommand>
    {
        public CadastrarEnqueteCommandValidator()
        {
            RuleFor(d => d.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(100).WithMessage("O campo {PropertyName} não pode ter mais de 100 caracteres");

            RuleFor(d => d.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(500).WithMessage("O campo {PropertyName} não pode ter mais de 500 caracteres");

            RuleFor(d => d.Encerramento)
                .NotNull().WithMessage("O campo {PropertyName} é obrigatório")
                .GreaterThan(DateTime.Now).WithMessage("A data de encerramento deve ser maior que a data atual");

            RuleFor(d => d.Opcoes)
                .NotNull().WithMessage("Você deve fornecer as opções da enquete")
                .Must(o => o.Count >= 3).WithMessage("A enquete deve ter no mínimo 3 opções");
        }
    }
}
