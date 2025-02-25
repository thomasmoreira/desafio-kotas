using Mapster;
using MediatR;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Commands;
using Pokemon.Application.Services;
using Pokemon.Domain.Entities;

namespace Pokemon.Application.Features.Pokemons.Handlers;

public class CaptureCreateCommandHandler : IRequestHandler<CaptureCreateCommand, CaptureResponseDto>
{
    private readonly ICaptureService _captureService;

    public CaptureCreateCommandHandler(ICaptureService captureService)
    {
        _captureService = captureService;
    }

    public async Task<CaptureResponseDto> Handle(CaptureCreateCommand request, CancellationToken cancellationToken)
    {

        var captureEntity = request.CaptureDto.Adapt<PokemonCapture>();

        captureEntity.CaptureDate = DateTime.UtcNow;

        var resultEntity = await _captureService.AddCaptureAsync(captureEntity);

        var responseDto = resultEntity.Adapt<CaptureResponseDto>();
        return responseDto;
    }
}
