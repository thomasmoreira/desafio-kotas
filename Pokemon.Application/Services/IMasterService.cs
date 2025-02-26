using Pokemon.Domain.Entities;

namespace Pokemon.Application.Services;

public interface IMasterService
{
    Task<PokemonMaster> CreateMasterAsync(PokemonMaster master);
    Task<PokemonMaster?> GetPokemonMasterById(Guid id);
    Task<IEnumerable<PokemonMaster>> GetPokemonMastersPaginatedAsync(int pageNumber, int pageSize);
}
