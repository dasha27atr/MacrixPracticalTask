using MacrixPracticalTask.Config;
using MacrixPracticalTask.Context;
using MacrixPracticalTask.Logger;
using MacrixPracticalTask.Logger.ILog;
using MacrixPracticalTask.Repository;
using MacrixPracticalTask.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

configureServices(builder.Services, builder.Configuration);

var app = builder.Build();

configureApp(app);

app.Run();

static void configureServices(IServiceCollection services, ConfigurationManager configuration)
{
    // Add services to the container.
    services.AddControllers();

    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddAutoMapper(typeof(Program));

    services.AddSingleton<ILoggerManager, LoggerManager>();

    services.AddSingleton(x => {
        var json = File.ReadAllText("appsettings.json");
        Config config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(json) ??
            throw new Exception("Unable to read configuration file.");
        return config;
    });

    services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
    {
        var config = serviceProvider.GetRequiredService<Config>();

        var connection = new SqliteConnection(config.ConnectionString);
        connection.Open();
        connection.EnableExtensions(true);
        
        options.UseSqlite(connection);
    }, ServiceLifetime.Transient);

    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "MacrixPracticalTask",
            Description = "API for MacrixPracticalTask",
        });
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
}

static void configureApp(WebApplication app)
{
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureCreated();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        });
    }
}
