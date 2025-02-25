﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PokemonApi.Controllers
{
    public class BaseApiController : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
