namespace Pokemon.Domain.Entities;

public class PokemonCapture
{
    public int Id { get; set; }
    public int PokemonId { get; set; }
    public int MasterId { get; set; }
    public DateTime CaptureDate { get; set; } = DateTime.UtcNow;
    public PokemonMaster? Master { get; set; }
}
