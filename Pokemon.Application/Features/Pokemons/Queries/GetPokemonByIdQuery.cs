using MediatR;
using Pokemon.Application.DTOs;

namespace Pokemon.Application.Features.Pokemons.Queries
{
    public class GetPokemonByIdQuery : IRequest<PokemonDto>
    {
        public int Id { get; }

        public GetPokemonByIdQuery(int id)
        {
            Id = id;
        }
    }
}
