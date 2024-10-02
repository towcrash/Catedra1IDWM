using api.src.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<DataContext> (options => options.UseSqlite("Data Source=Catedra1IDWM.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope()) // Se levanta la base de datos
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>(); // Obtener servicio 
    await context.Database.MigrateAsync(); // Se realiza la migración de la base de datos de forma asincrona
    
}



app.Run();

