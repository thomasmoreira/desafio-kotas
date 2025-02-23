using Pokemon.Application.Services;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace Pokemon.Infraestructure.Services;

public class PokeApiService : IPokeApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://pokeapi.co/api/v2";

    public PokeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JsonNode?> GetPokemonByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/pokemon/{id}");
        if (!response.IsSuccessStatusCode)
            return null;
        var json = await response.Content.ReadAsStringAsync();
        return JsonNode.Parse(json);
    }

    public async Task<IEnumerable<JsonNode?>> GetRandomPokemonsAsync(int count)
    {
        // Primeiro, obtém a quantidade total de pokemons
        var response = await _httpClient.GetAsync($"{BaseUrl}/pokemon?limit=1");
        if (!response.IsSuccessStatusCode)
            return Enumerable.Empty<JsonNode?>();

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);
        int total = doc.RootElement.GetProperty("count").GetInt32();

        var random = new Random();
        var tasks = new List<Task<JsonNode?>>();
        var generatedIds = new HashSet<int>();

        // Garante que não haja repetição
        while (generatedIds.Count < count)
        {
            int id = random.Next(1, total + 1);
            if (generatedIds.Add(id))
            {
                tasks.Add(GetPokemonByIdAsync(id));
            }
        }

        return await Task.WhenAll(tasks);
    }
}
