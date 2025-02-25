namespace Pokemon.Application.DTOs;

public class CaptureResponseDto
{
    public Guid Id { get; set; }
    public int PokemonId { get; set; }    
    public Guid MasterId { get; set; }
    public DateTime CaptureDate { get; set; }
}
