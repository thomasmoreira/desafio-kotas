using Pokemon.Domain.Entities;

namespace Pokemon.Application.Services;

public interface IMasterService
{
    Task<PokemonMaster> CreateMasterAsync(PokemonMaster master);
}
