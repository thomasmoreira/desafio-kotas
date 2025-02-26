namespace Pokemon.Application.DTOs;

public class CaptureRequestDto
{
    public int PokemonId { get; set; }
    public Guid MasterId { get; set; }
}
