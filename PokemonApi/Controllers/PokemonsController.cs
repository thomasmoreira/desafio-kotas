using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.Services;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonsController : ControllerBase
{
    private readonly IPokeApiService _pokeApiService;
    public PokemonsController(IPokeApiService pokeApiService)
    {
        _pokeApiService = pokeApiService;
    }

    // GET api/pokemons/random
    [HttpGet("random")]
    public async Task<IActionResult> GetRandomPokemons()
    {
        var pokemons = await _pokeApiService.GetRandomPokemonsAsync(10);
        return Ok(pokemons);
    }

    // GET api/pokemons/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPokemonById(int id)
    {
        var pokemon = await _pokeApiService.GetPokemonByIdAsync(id);
        if (pokemon == null)
            return NotFound();
        return Ok(pokemon);
    }
}
