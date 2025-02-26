using Pokemon.Application.DTOs;
using Pokemon.Application.Services;
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

    public async Task<PokemonDto?> GetPokemonByIdAsync(int id)
    {

        var pokemon = await GetPokemonDetailsAsync(id);
        return pokemon;
    }

    public async Task<IEnumerable<PokemonDto?>> GetRandomPokemonsAsync(int count)
    {
        // Obtém o total de pokémons para gerar IDs aleatórios
        var response = await _httpClient.GetAsync($"{BaseUrl}/pokemon?limit=1");
        if (!response.IsSuccessStatusCode)
            return Enumerable.Empty<PokemonDto>();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        int total = doc.RootElement.GetProperty("count").GetInt32();

        var validPokemons = new List<PokemonDto>();
        var random = new Random();

        while (validPokemons.Count < count)
        {
            int id = random.Next(1, total + 1);
            var pokemon = await GetPokemonDetailsAsync(id);
            if (pokemon != null)
            {
                validPokemons.Add(pokemon);
            }
        }
        return validPokemons;
    }

    public async Task<PokemonDto?> GetPokemonDetailsAsync(int id)
    {
        // Obter dados básicos do Pokémon
        var response = await _httpClient.GetAsync($"{BaseUrl}/pokemon/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        var pokemonCreature = new PokemonDto
        {
            Id = root.GetProperty("id").GetInt32(),
            Nome = root.GetProperty("name").GetString() ?? string.Empty
        };

        var spriteUrl = root.GetProperty("sprites").GetProperty("front_default").GetString();
        if (!string.IsNullOrEmpty(spriteUrl))
        {
            pokemonCreature.SpriteBase64 = await GetImageAsBase64Async(spriteUrl);
        }

        var speciesUrl = root.GetProperty("species").GetProperty("url").GetString();
        if (!string.IsNullOrEmpty(speciesUrl))
        {
            var speciesResponse = await _httpClient.GetAsync(speciesUrl);
            if (speciesResponse.IsSuccessStatusCode)
            {
                var speciesJson = await speciesResponse.Content.ReadAsStringAsync();
                using var speciesDoc = JsonDocument.Parse(speciesJson);
                var evolutionChainUrl = speciesDoc.RootElement
                    .GetProperty("evolution_chain")
                    .GetProperty("url")
                    .GetString();

                if (!string.IsNullOrEmpty(evolutionChainUrl))
                {
                    var evolutionResponse = await _httpClient.GetAsync(evolutionChainUrl);
                    if (evolutionResponse.IsSuccessStatusCode)
                    {
                        var evolutionJson = await evolutionResponse.Content.ReadAsStringAsync();
                        using var evolutionDoc = JsonDocument.Parse(evolutionJson);

                        pokemonCreature.Evolucoes = ParseEvolutionChain(evolutionDoc.RootElement);
                    }
                }
            }
        }

        return pokemonCreature;
    }

    private async Task<string> GetImageAsBase64Async(string imageUrl)
    {
        var imageResponse = await _httpClient.GetAsync(imageUrl);
        if (!imageResponse.IsSuccessStatusCode)
            return string.Empty;

        var imageBytes = await imageResponse.Content.ReadAsByteArrayAsync();
        return Convert.ToBase64String(imageBytes);
    }

    private List<string> ParseEvolutionChain(JsonElement evolutionChainRoot)
    {
        var evolucoes = new List<string>();

        if (evolutionChainRoot.TryGetProperty("chain", out var chain))
        {
            void Traverse(JsonElement node)
            {
                if (node.TryGetProperty("species", out var species))
                {
                    var nome = species.GetProperty("name").GetString();
                    if (!string.IsNullOrEmpty(nome) && !evolucoes.Contains(nome))
                    {
                        evolucoes.Add(nome);
                    }
                }

                if (node.TryGetProperty("evolves_to", out var evolvesTo))
                {
                    foreach (var child in evolvesTo.EnumerateArray())
                    {
                        Traverse(child);
                    }
                }
            }

            Traverse(chain);
        }
        return evolucoes;
    }

}
