using Mapster;
using Microsoft.EntityFrameworkCore;
using Pokemon.Application.Common.Models;
using Pokemon.Application.DTOs;
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

    public async Task<IEnumerable<PokemonCapture>> GetCapturesAsync(int pageNumber, int pageSize)
    {
        var query = _context.PokemonCaptures
                            .AsNoTracking()
                            .OrderBy(c => c.Id);

        int totalCount = await query.CountAsync();

        var captures = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return captures;
    }
}