namespace Pokemon.Application.DTOs
{
    public class PokemonMasterResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Cpf { get; set; } = string.Empty;
    }
}
