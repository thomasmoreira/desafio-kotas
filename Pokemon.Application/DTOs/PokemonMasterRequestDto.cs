namespace Pokemon.Application.DTOs
{
    public class PokemonMasterRequestDto
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        public string Cpf { get; set; } = string.Empty;
    }
}
