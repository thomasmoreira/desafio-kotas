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
        // Mapeia o DTO para a entidade. 
        // Assumindo que o mapeamento foi configurado para definir DataCaptura como DateTime.UtcNow.
        var captureEntity = request.CaptureDto.Adapt<PokemonCapture>();

        // Se não configurou o mapping, defina manualmente:
        captureEntity.DataCaptura = DateTime.UtcNow;

        var resultEntity = await _captureService.AddCaptureAsync(captureEntity);

        // Mapeia a entidade persistida para o DTO de resposta
        var responseDto = resultEntity.Adapt<CaptureResponseDto>();
        return responseDto;
    }
}
