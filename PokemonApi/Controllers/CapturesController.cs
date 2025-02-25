using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.Common.Models;
using Pokemon.Application.DTOs;
using Pokemon.Application.Features.Pokemons.Commands;
using Pokemon.Application.Features.Pokemons.Queries;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CapturesController : BaseApiController
{        
    [HttpPost]
    public async Task<IActionResult> AddCapture([FromBody] CaptureRequestDto capture)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var command = new CaptureCreateCommand(capture);
        
        CaptureResponseDto result = await Mediator.Send(command);
        
        return CreatedAtAction(nameof(AddCapture), new { id = result.Id }, result);
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetCaptures([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetCapturesQuery(pageNumber, pageSize);
        PaginatedResponse<CaptureResponseDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}
