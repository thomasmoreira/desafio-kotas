using Microsoft.EntityFrameworkCore;
using Pokemon.Application;
using Pokemon.Application.Features.Pokemons.Handlers;
using Pokemon.Application.Services;
using Pokemon.Infraestructure.Persistence.Context;
using Pokemon.Infraestructure.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configurar serviços EF Core com SQLite
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ??
                "Data Source=pokeapi.db"));

        builder.Services.AddScoped<IPokeApiService, PokeApiService>();
        builder.Services.AddScoped<IMasterService, MasterService>();
        builder.Services.AddScoped<ICaptureService, CaptureService>();
        builder.Services.AddHttpClient<IPokeApiService, PokeApiService>();

        builder.Services.AddApplication();


        builder.Services.AddControllers();
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                return Task.CompletedTask;
            });
        });

        builder.Services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.ToString()); // Handle SchemaId already used for different type
        });
        //builder.Services.TryAddSwagger(_useSwagger);

        var app = builder.Build();

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