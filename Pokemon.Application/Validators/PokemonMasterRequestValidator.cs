using FluentValidation;
using Pokemon.Application.DTOs;

namespace Pokemon.Application.Validators;

public class PokemonMasterRequestValidator : AbstractValidator<PokemonMasterRequestDto>
{
    public PokemonMasterRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve conter pelo menos 3 caracteres.");

        RuleFor(x => x.Idade)
            .GreaterThan(0).WithMessage("A idade deve ser maior que zero.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Matches(@"^\d{11}$").WithMessage("O CPF deve conter 11 dígitos numéricos.");
    }
}
