﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pokemon.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        private ISender _mediator = null!;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
