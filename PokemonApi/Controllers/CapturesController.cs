using Microsoft.AspNetCore.Mvc;
using Pokemon.Application.Services;
using Pokemon.Domain.Entities;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CapturesController : ControllerBase
{
    private readonly ICaptureService _captureService;
    public CapturesController(ICaptureService captureService)
    {
        _captureService = captureService;
    }

    // POST api/captures
    [HttpPost]
    public async Task<IActionResult> AddCapture([FromBody] PokemonCapture capture)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newCapture = await _captureService.AddCaptureAsync(capture);
        return CreatedAtAction(nameof(AddCapture), new { id = newCapture.Id }, newCapture);
    }

    // GET api/captures
    [HttpGet]
    public async Task<IActionResult> GetCaptures()
    {
        var captures = await _captureService.GetCapturesAsync();
        return Ok(captures);
    }
}
