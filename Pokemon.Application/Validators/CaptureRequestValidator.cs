using FluentValidation;
using Pokemon.Application.DTOs;
using Pokemon.Application.Services;

namespace Pokemon.Application.Validators;

public class CaptureRequestValidator : AbstractValidator<CaptureRequestDto>
{
    private readonly ICaptureService _captureService;
    private readonly IPokeApiService _pokeApiService;
    private readonly IMasterService _masterService;
    public CaptureRequestValidator(ICaptureService captureService, IPokeApiService pokeApiService, IMasterService masterService)
    {
        _captureService = captureService;
        _pokeApiService = pokeApiService;
        _masterService = masterService;


        RuleFor(x => x.PokemonId)
              .NotEmpty().WithMessage("O Id do Pokémon é obrigatório.")
              .GreaterThan(0).WithMessage("O Id do Pokémon deve ser maior que zero.");

        RuleFor(x => x.PokemonId).MustAsync(async (pokemonId, cancellationToken) =>
        {
            var pokemon = await _pokeApiService.GetPokemonByIdAsync(pokemonId);
            return pokemon != null;
        }).WithMessage("Informe um pokemon válido.");

        RuleFor(x => x.PokemonId).MustAsync(async (pokemonId, cancellationToken) =>
        {
            var capture = await _captureService.GetCaptureAsync(pokemonId);
            return capture == null;
        }).WithMessage("Você já realizou essa captura.");


        RuleFor(x => x.MasterId)
               .NotEmpty().WithMessage("O Id do Mestre Pokémon é obrigatório.");

        RuleFor(x => x.MasterId).MustAsync(async (masterId, cancellationToken) =>
        {
            var master = await _masterService.GetPokemonMasterById(masterId);
            return master != null;
        }).WithMessage("Informe um Mestre Pokémon válido.");
        
    }
}
