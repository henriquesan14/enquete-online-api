using EnqueteOnline.Application.Commands.CadastrarUsuario;
using FluentValidation;

namespace EnqueteOnline.Application.Validators
{
    public class CadastrarUsuarioCommandValidator : AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidator()
        {
            RuleFor(d => d.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(30).WithMessage("O campo {PropertyName} não pode ter mais de 30 caracteres");

            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .EmailAddress().WithMessage("O campo {PropertyName} tem que ser um email válido.");
        }
    }
}
