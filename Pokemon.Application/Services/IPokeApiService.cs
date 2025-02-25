using Pokemon.Application.DTOs;
using Pokemon.Domain.Entities;

namespace Pokemon.Application.Services;

public interface IPokeApiService
{
    Task<PokemonDto?> GetPokemonByIdAsync(int id);
    Task<IEnumerable<PokemonDto?>> GetRandomPokemonsAsync(int count);
    Task<PokemonDto?> GetPokemonDetailsAsync(int id);
}