using Mapster;
using MediatR;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Queries;
using Pokemon.Application.Services;

namespace Pokemon.Application.Features.Pokemons.Handlers;

public class GetPokemonByIdQueryHandler : IRequestHandler<GetPokemonByIdQuery, PokemonDto>
{
    private readonly IPokeApiService _pokeApiService;

    public GetPokemonByIdQueryHandler(IPokeApiService pokeApiService)
    {
        _pokeApiService = pokeApiService;
    }

    public async Task<PokemonDto?> Handle(GetPokemonByIdQuery request, CancellationToken cancellationToken)
    {
        var pokemon = await _pokeApiService.GetPokemonDetailsAsync(request.Id);
        var pokemonDto = pokemon?.Adapt<PokemonDto>();
        return pokemonDto;
    }
}
