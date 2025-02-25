using MediatR;
using Pokemon.Application.Common.Models;
using Pokemon.Application.DTOs;

namespace Pokemon.Application.Features.Pokemons.Queries
{
    public class GetPokemonMastersQuery : IRequest<PaginatedResponse<PokemonMasterResponseDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPokemonMastersQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
