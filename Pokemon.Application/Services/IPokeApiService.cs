using System.Text.Json.Nodes;

namespace Pokemon.Application.Services;

public interface IPokeApiService
{
    Task<JsonNode?> GetPokemonByIdAsync(int id);
    Task<IEnumerable<JsonNode?>> GetRandomPokemonsAsync(int count);
}