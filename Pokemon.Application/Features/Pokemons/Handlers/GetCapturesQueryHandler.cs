using Mapster;
using MediatR;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Queries;
using Pokemon.Application.Responses;
using Pokemon.Application.Services;

namespace Pokemon.Application.Features.Pokemons.Handlers;

public class GetCapturesQueryHandler : IRequestHandler<GetCapturesQuery, PaginatedResponse<CaptureResponseDto>>
{
    private readonly ICaptureService _captureService;

    public GetCapturesQueryHandler(ICaptureService captureService)
    {
        _captureService = captureService;
    }

    public async Task<PaginatedResponse<CaptureResponseDto>> Handle(GetCapturesQuery request, CancellationToken cancellationToken)
    {
        // Obtém todas as capturas
        var allCaptures = await _captureService.GetCapturesAsync();
        int totalCount = allCaptures.Count();

        // Aplica a paginação (assumindo que PageNumber começa em 1)
        var pagedCaptures = allCaptures
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        // Mapeia para o DTO de resposta usando Mapster
        var dtos = pagedCaptures.Adapt<List<CaptureResponseDto>>();

        return new PaginatedResponse<CaptureResponseDto>
        {
            Items = dtos,
            TotalCount = totalCount
        };
    }
}
