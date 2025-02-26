using Pokemon.Domain.Entities;

namespace Pokemon.Application.Services;

public interface ICaptureService
{
    Task<PokemonCapture> AddCaptureAsync(PokemonCapture capture);
    Task<IEnumerable<PokemonCapture>> GetCapturesAsync(int pageNumber, int pageSize);
    Task<PokemonCapture?> GetCaptureAsync(int pokemonId);
}
