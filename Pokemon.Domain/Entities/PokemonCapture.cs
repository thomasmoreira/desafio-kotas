namespace Pokemon.Domain.Entities;

public class PokemonCapture : BaseEntity
{
    public int PokemonId { get; set; }
    public Guid MasterId { get; set; }
    public DateTime CaptureDate { get; set; } = DateTime.UtcNow;
    public PokemonMaster? Master { get; set; }
}
