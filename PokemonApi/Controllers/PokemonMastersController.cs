using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Commands;
using Pokemon.Application.Services;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonMastersController : BaseApiController
{    
    [HttpPost]
    public async Task<IActionResult> CreateMaster([FromBody] PokemonMasterRequestDto master)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreatePokemonMasterCommand(master);
        var result = await Mediator.Send(command);

        return CreatedAtAction(nameof(CreateMaster), new { id = result.Id }, result);
    }
}
