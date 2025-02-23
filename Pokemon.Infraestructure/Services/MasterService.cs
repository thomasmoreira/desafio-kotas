using Pokemon.Application.Services;
using Pokemon.Domain.Entities;
using Pokemon.Infraestructure.Persistence.Context;

namespace Pokemon.Infraestructure.Services;

public class MasterService : IMasterService
{
    private readonly ApplicationDbContext _context;
    public MasterService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PokemonMaster> CreateMasterAsync(PokemonMaster master)
    {
        _context.PokemonMasters.Add(master);
        await _context.SaveChangesAsync();
        return master;
    }
}
