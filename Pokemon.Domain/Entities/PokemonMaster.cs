namespace Pokemon.Domain.Entities;

public  class PokemonMaster : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Cpf { get; set; } = string.Empty;
}
