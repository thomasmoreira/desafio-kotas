using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Pokemon.Api.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value.Errors.Select(e => e.ErrorMessage)
                });

            context.Result = new BadRequestObjectResult(new
            {
                Message = "Um ou mais erros de validação ocorreram.",
                Errors = errors
            });
        }   
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
