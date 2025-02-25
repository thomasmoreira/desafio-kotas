﻿using MediatR;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Queries;
using Pokemon.Application.Services;

namespace Pokemon.Application.Features.Pokemons.Handlers;

public class GetRandomPokemonsQueryHandler : IRequestHandler<GetRandomPokemonsQuery, IEnumerable<PokemonDto>>
{
    private readonly IPokeApiService _pokeApiService;

    public GetRandomPokemonsQueryHandler(IPokeApiService pokeApiService)
    {
        _pokeApiService = pokeApiService;
    }

    public async Task<IEnumerable<PokemonDto>> Handle(GetRandomPokemonsQuery request, CancellationToken cancellationToken)
    {
        // Busca os Pokémon aleatórios conforme a quantidade solicitada
        var pokemons = await _pokeApiService.GetRandomPokemonsAsync(request.Count);
        // Se for necessário mapear, utilize Mapster aqui:
        // var dtos = pokemons.Adapt<List<PokemonDto>>();
        return pokemons;
    }
}
