using FluentValidation;
using Pokemon.Application.DTOs;
using Pokemon.Application.Services;

namespace Pokemon.Application.Validators;

public class CaptureCreateRequestValidator : AbstractValidator<CaptureRequestDto>
{
    private readonly ICaptureService _captureService;
    public CaptureCreateRequestValidator(ICaptureService captureService)
    {
        _captureService = captureService;


        RuleFor(x => x.PokemonId)
              .NotEmpty().WithMessage("O Id do Pokémon é obrigatório.")
              .GreaterThan(0).WithMessage("O Id do Pokémon deve ser maior que zero.");

        RuleFor(x => x.MasterId)
               .NotEmpty().WithMessage("O Id do Mestre Pokémon é obrigatório.");

        RuleFor(x => x.PokemonId).MustAsync(async (pokemonId, cancellationToken) =>
        {
            var capture = await _captureService.GetCaptureAsync(pokemonId);
            return capture == null;
        }).WithMessage("Você já realizou essa captura");
        
    }
}
