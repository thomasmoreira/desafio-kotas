using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.Common.Models;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Commands;
using Pokemon.Application.Features.Pokemons.Queries;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonMastersController : BaseApiController
{    
    [HttpPost]
    public async Task<IActionResult> CreateMaster([FromBody] PokemonMasterRequestDto master)
    {        
        var command = new CreatePokemonMasterCommand(master);
        var result = await Mediator.Send(command);

        return CreatedAtAction(nameof(CreateMaster), new { id = result.Id }, result);
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetMastersPaginated([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetPokemonMastersQuery(pageNumber, pageSize);
        PaginatedResponse<PokemonMasterResponseDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}
