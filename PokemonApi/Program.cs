using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokemon.Application;
using Pokemon.Application.Services;
using Pokemon.Application.Validators;
using Pokemon.Infraestructure;
using Pokemon.Infraestructure.Persistence.Context;
using Pokemon.Infraestructure.Services;
using PokemonApi.Filters;
using PokemonApi.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ??
                "Data Source=pokeapi.db"));

        builder.Services.AddApplication();
        builder.Services.AddInfraestructure();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });
        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                return Task.CompletedTask;
            });
        });

        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString());
        });
        
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/openapi/v1.json", ""); });
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}