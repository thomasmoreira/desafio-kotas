using Mapster;
using MediatR;
using Pokemon.Application.Common.Models;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Queries;
using Pokemon.Application.Services;

namespace Pokemon.Application.Features.Pokemons.Handlers;

public class GetPokemonMastersQueryHandler : IRequestHandler<GetPokemonMastersQuery, PaginatedResponse<PokemonMasterResponseDto>>
{
    private readonly IMasterService _masterService;

    public GetPokemonMastersQueryHandler(IMasterService masterService)
    {
        _masterService = masterService;
    }

    public async Task<PaginatedResponse<PokemonMasterResponseDto>> Handle(GetPokemonMastersQuery request, CancellationToken cancellationToken)
    {

        var allMasters = await _masterService.GetPokemonMastersPaginatedAsync(request.PageNumber, request.PageSize);
                
        var mastersDto = allMasters.Adapt<IEnumerable<PokemonMasterResponseDto>>();

        return new PaginatedResponse<PokemonMasterResponseDto>
        {
            Items = mastersDto,
            Page = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = mastersDto.Count()
        };
    }
}
