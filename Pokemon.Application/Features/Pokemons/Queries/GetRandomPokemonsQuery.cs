using MediatR;
using Pokemon.Application.DTOs;

namespace Pokemon.Application.Features.Pokemons.Queries;

public class GetRandomPokemonsQuery : IRequest<IEnumerable<PokemonDto>>
{
    public int Count { get; }

    public GetRandomPokemonsQuery(int count)
    {
        Count = count;
    }
}
