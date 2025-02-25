namespace Pokemon.Application.DTOs;

public class CaptureResponseDto
{
    public int Id { get; set; }
    public int PokemonId { get; set; }
    public int MasterId { get; set; }
    public DateTime DataCaptura { get; set; }
}
