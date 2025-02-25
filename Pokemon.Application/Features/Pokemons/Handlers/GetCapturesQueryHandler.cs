using Mapster;
using MediatR;
using Pokemon.Application.Common.Models;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Queries;
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

        var allCaptures = await _captureService.GetCapturesAsync(request.PageNumber, request.PageSize);

        var capturesDto = allCaptures.Adapt<IEnumerable<CaptureResponseDto>>();

        return new PaginatedResponse<CaptureResponseDto> 
        {
            Items = capturesDto,
            TotalCount = allCaptures.Count(),
            Page = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
