namespace Pokemon.Application.DTOs;

public class PokemonDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    // Outras propriedades básicas, como tipos, habilidades, etc.
    public List<string> Evolucoes { get; set; } = new List<string>();
    public string SpriteBase64 { get; set; } = string.Empty;
}
