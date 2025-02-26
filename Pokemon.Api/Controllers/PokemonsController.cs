using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.Features.Pokemons.Queries;

namespace Pokemon.Api.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
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
            return NotFound("Pokemon não encontrado");

        return Ok(pokemon);
    }
}
