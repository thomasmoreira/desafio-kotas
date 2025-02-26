using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Pokemon.Api.Filters;

public class ValidateModelAsyncAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {        
        foreach (var argument in context.ActionArguments)
        {            
            var validatorType = typeof(IValidator<>).MakeGenericType(argument.Value.GetType());
            var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument.Value));
                if (!validationResult.IsValid)
                {                    
                    context.Result = new BadRequestObjectResult(new
                    {
                        Message = "Um ou mais erros de validação ocorreram.",
                        Errors = validationResult.Errors.Select(e => new
                        {
                            Field = e.PropertyName,
                            e.ErrorMessage
                        })
                    });
                    return;
                }
            }
        }

        await next();
    }
}
