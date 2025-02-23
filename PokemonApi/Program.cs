using Microsoft.EntityFrameworkCore;
using Pokemon.Application.Services;
using Pokemon.Infraestructure.Persistence.Context;
using Pokemon.Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar serviços EF Core com SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ??
        "Data Source=pokeapi.db"));

builder.Services.AddScoped<IPokeApiService, PokeApiService>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<ICaptureService, CaptureService>();

builder.Services.AddHttpClient<IPokeApiService, PokeApiService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
}

app.UseAuthorization();

app.MapControllers();

app.Run();
