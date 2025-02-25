using Pokemon.Application.Services;
using System.Text.Json;
using Pokemon.Application.DTOs;

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

        var dto = new PokemonDto
        {
            Id = root.GetProperty("id").GetInt32(),
            Nome = root.GetProperty("name").GetString() ?? string.Empty
            // Preencha outras propriedades básicas conforme necessário
        };

        // Converter sprite default em base64
        var spriteUrl = root.GetProperty("sprites").GetProperty("front_default").GetString();
        if (!string.IsNullOrEmpty(spriteUrl))
        {
            dto.SpriteBase64 = await GetImageAsBase64Async(spriteUrl);
        }

        // Buscar dados da espécie para obter a URL da cadeia de evolução
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

                        // Processa a cadeia de evolução para extrair os nomes
                        dto.Evolucoes = ParseEvolutionChain(evolutionDoc.RootElement);
                    }
                }
            }
        }

        return dto;
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

        // A estrutura da cadeia de evolução começa em "chain"
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

                // Se existir evolução, ela geralmente está no array "evolves_to"
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
