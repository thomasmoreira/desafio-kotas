using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<PokemonMaster>> GetPokemonMastersPaginatedAsync(int pageNumber, int pageSize)
    {
        
        var query = _context.PokemonMasters
                            .AsNoTracking()
                            .OrderBy(m => m.Id);
        
        int totalCount = await query.CountAsync();

        // Aplica paginação com Skip e Take de forma assíncrona
        var masters = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return masters;

        
    }
}
