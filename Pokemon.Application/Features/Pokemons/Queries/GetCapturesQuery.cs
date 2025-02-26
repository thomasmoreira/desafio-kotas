using MediatR;
using Pokemon.Application.Common.Models;
using Pokemon.Application.DTOs;

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
