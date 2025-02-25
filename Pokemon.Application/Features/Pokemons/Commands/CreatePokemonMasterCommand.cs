using MediatR;
using Pokemon.Application.DTOs;

namespace Pokemon.Application.Features.Pokemons.Commands
{
    public class CreatePokemonMasterCommand : IRequest<PokemonMasterResponseDto>
    {
        public PokemonMasterRequestDto MasterRequest { get; }

        public CreatePokemonMasterCommand(PokemonMasterRequestDto masterRequest)
        {
            MasterRequest = masterRequest;
        }
    }
}
