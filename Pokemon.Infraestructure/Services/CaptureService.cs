using Microsoft.EntityFrameworkCore;
using Pokemon.Application.Services;
using Pokemon.Domain.Entities;
using Pokemon.Infraestructure.Persistence.Context;

namespace Pokemon.Infraestructure.Services;

public class CaptureService : ICaptureService
{
    private readonly ApplicationDbContext _context;
    public CaptureService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PokemonCapture> AddCaptureAsync(PokemonCapture capture)
    {
        _context.PokemonCaptures.Add(capture);
        await _context.SaveChangesAsync();
        return capture;
    }

    public async Task<IEnumerable<PokemonCapture>> GetCapturesAsync()
    {
        return await _context.PokemonCaptures
            .Include(c => c.Master)
            .ToListAsync();
    }
}