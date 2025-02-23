using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.Services;
using Pokemon.Domain.Entities;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonMastersController : ControllerBase
{
    private readonly IMasterService _masterService;
    public PokemonMastersController(IMasterService masterService)
    {
        _masterService = masterService;
    }

    // POST api/pokemonmasters
    [HttpPost]
    public async Task<IActionResult> CreateMaster([FromBody] PokemonMaster master)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdMaster = await _masterService.CreateMasterAsync(master);
        return CreatedAtAction(nameof(CreateMaster), new { id = createdMaster.Id }, createdMaster);
    }
}
