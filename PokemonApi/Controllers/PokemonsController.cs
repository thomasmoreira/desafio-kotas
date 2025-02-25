using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.Features.Pokemons.Queries;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonsController : BaseApiController
{    
    [HttpGet("random")]
    public async Task<IActionResult> GetRandomPokemons(int count = 10)
    {
        var query = new GetRandomPokemonsQuery(count);
        var pokemons = await Mediator.Send(query);
        return Ok(pokemons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPokemonById(int id)
    {
        var query = new GetPokemonByIdQuery(id);
        var pokemon = await Mediator.Send(query);
        if (pokemon == null)
            return NotFound();

        return Ok(pokemon);
    }
}
