using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;
using Service;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<MyDbContext>(config =>
        {
            config.UseSqlite("Data Source=pets.db");
        });
        builder.Services.AddScoped<PetService>();
        builder.Services.AddOpenApiDocument();
        builder.Services.AddControllers();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
           var dbcontext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
           dbcontext.Database.EnsureCreated();
        }

        app.UseOpenApi();
        app.UseSwaggerUi();

        app.MapControllers();
        app.Run();
    }
}