using MediatR;
using Pokemon.Application.DTOs;
using Pokemon.Application.Responses;

namespace Pokemon.Application.Features.Pokemons.Queries;

public class GetCapturesQuery : IRequest<PaginatedResponse<CaptureResponseDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetCapturesQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
