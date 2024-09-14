using DotNetEnv;
using ecommerce.DAL.Repository;
using ecommerce.DAL.Repository.Contrato;
using ecommerce.IOC;
using ecommerce.UTILIY;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables del archivo .env
Env.Load();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyeccion dependecia acorde con la dependencia 
builder.Services.InyectionDependecy();
 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
