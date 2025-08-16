using DotNetEnv;

using GestiónDeImagenIA_Back.BusinessLogic.Interfaz;
using GestiónDeImagenIA_Back.BusinessLogic.Logic;
using GestiónDeImagenIA_Back.Core;
using GestiónDeImagenIA_Back.Core.Repositories;
using GestiónDeImagenIA_Back.Services;
using GestiónDeImagenIA_Back.Services.Interfaz;
using Microsoft.EntityFrameworkCore;
using Refit;


var builder = WebApplication.CreateBuilder(args);
Env.Load();
var url_front = Environment.GetEnvironmentVariable("URL_FRONT");
builder.Configuration.AddEnvironmentVariables();




builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("MARIADB_CONNECTION");
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
});
// 1. Habilitar CORS para permitir llamadas desde el frontend (Vite)
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(url_front) // URL del Front
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Add services to the container.



builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IComprobanteRepository, ComprobanteRepository>();
builder.Services.AddScoped<IComprobanteLogic, ComprobanteLogic>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRefitClient<IGeminics>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://generativelanguage.googleapis.com");
        client.Timeout = TimeSpan.FromSeconds(30);
    });
builder.Services.AddScoped<Igeminis, GeminiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.UseRouting();

app.MapControllers();

app.Run();
