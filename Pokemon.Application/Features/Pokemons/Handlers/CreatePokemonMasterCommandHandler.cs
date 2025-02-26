using Mapster;
using MediatR;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Commands;
using Pokemon.Application.Services;
using Pokemon.Domain.Entities;

namespace Pokemon.Application.Features.Pokemons.Handlers;

public class CreatePokemonMasterCommandHandler : IRequestHandler<CreatePokemonMasterCommand, PokemonMasterResponseDto>
{
    private readonly IMasterService _masterService;

    public CreatePokemonMasterCommandHandler(IMasterService masterService)
    {
        _masterService = masterService;
    }

    public async Task<PokemonMasterResponseDto> Handle(CreatePokemonMasterCommand request, CancellationToken cancellationToken)
    {        
        var masterEntity = request.MasterRequest.Adapt<PokemonMaster>();
     
        var createdMaster = await _masterService.CreateMasterAsync(masterEntity);

        var responseDto = createdMaster.Adapt<PokemonMasterResponseDto>();
        return responseDto;
    }
}
