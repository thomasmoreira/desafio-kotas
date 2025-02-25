using Pokemon.Application.Common.Models;
using Pokemon.Application.DTOs;
using Pokemon.Domain.Entities;

namespace Pokemon.Application.Services;

public interface IMasterService
{
    Task<PokemonMaster> CreateMasterAsync(PokemonMaster master);
    Task<IEnumerable<PokemonMaster>> GetPokemonMastersPaginatedAsync(int pageNumber, int pageSize);
}
