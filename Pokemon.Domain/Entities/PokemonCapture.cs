namespace Pokemon.Domain.Entities;

public class PokemonCapture
{
    public int Id { get; set; }
    public int PokemonId { get; set; } // Id do Pokémon conforme a PokeAPI
    public int MasterId { get; set; }
    public DateTime DataCaptura { get; set; } = DateTime.UtcNow;

    // Navegação para o mestre
    public PokemonMaster? Master { get; set; }
}
