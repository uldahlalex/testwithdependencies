using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;
using Service;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);

        var app = builder.Build();

        ConfigureApp(app);
        
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<MyDbContext>(config =>
        {
            config.UseSqlite("Data Source=pets.db");
        });
        services.AddScoped<PetService>();
        services.AddOpenApiDocument();
        services.AddControllers();
    }

    public static void ConfigureApp(WebApplication app)
    {
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