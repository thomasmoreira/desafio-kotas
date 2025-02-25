using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Pokemon.Application;
using Pokemon.Application.Services;
using Pokemon.Application.Validators;
using Pokemon.Infraestructure.Persistence.Context;
using Pokemon.Infraestructure.Services;
using PokemonApi.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ??
                "Data Source=pokeapi.db"));

        builder.Services.AddScoped<IPokeApiService, PokeApiService>();
        builder.Services.AddScoped<IMasterService, MasterService>();
        builder.Services.AddScoped<ICaptureService, CaptureService>();
        builder.Services.AddHttpClient<IPokeApiService, PokeApiService>();
        builder.Services.AddApplication();
        builder.Services.AddControllers();
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
        // Configure the HTTP request pipeline.
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