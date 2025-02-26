using MediatR;
using Pokemon.Application.DTOs;

namespace Pokemon.Application.Features.Pokemons.Commands;

public class CaptureCreateCommand : IRequest<CaptureResponseDto>
{
    public CaptureRequestDto CaptureDto { get; set; }

    public CaptureCreateCommand(CaptureRequestDto captureDto)
    {
        CaptureDto = captureDto;
    }
}
