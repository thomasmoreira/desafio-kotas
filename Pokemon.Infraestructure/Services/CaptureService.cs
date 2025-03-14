﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<PokemonCapture?> GetCaptureAsync(int pokemonId)
    {
        var capture = await _context.PokemonCaptures            
            .FirstOrDefaultAsync(c => c.PokemonId == pokemonId);

        return capture;
    }

    public async Task<IEnumerable<PokemonCapture>> GetCapturesAsync(int pageNumber, int pageSize)
    {
        var query = _context.PokemonCaptures.Include(c => c.Master)
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