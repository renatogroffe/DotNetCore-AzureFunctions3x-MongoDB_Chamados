using FluentValidation;
using ServerlessSuporte.Models;

namespace ServerlessSuporte.Validators
{
    public class ChamadoValidator : AbstractValidator<Chamado>
    {
        public ChamadoValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Preencha o campo 'Nome'")
                .MinimumLength(4).WithMessage("O campo 'Nome' deve possuir no mínimo 4 caracteres")
                .MaximumLength(100).WithMessage("O campo 'Nome' deve possuir no máximo 100 caracteres");

            RuleFor(c => c.Email).NotEmpty().WithMessage("Preencha o campo 'Email'")
                .EmailAddress().WithMessage("Informado um conteúdo válido para o campo 'Email'");

            RuleFor(c => c.Descricao).NotEmpty().WithMessage("Preencha o campo 'Descricao'")
                .MinimumLength(20).WithMessage("O campo 'Descricao' deve possuir no mínimo 20 caracteres")
                .MaximumLength(1000).WithMessage("O campo 'Descricao' deve possuir no máximo 1000 caracteres");
        }
    }
}