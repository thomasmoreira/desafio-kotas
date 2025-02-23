namespace Pokemon.Domain.Entities;

public  class PokemonMaster
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public string Cpf { get; set; } = string.Empty;
}
