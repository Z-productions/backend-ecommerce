using DotNetEnv;
using ecommerce.IOC;

var builder = WebApplication.CreateBuilder(args);

// Cargar variables del archivo .env
Env.Load();

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inyección de dependencias
builder.Services.InyectionDependecy();
builder.Services.InyectionDependcyJwt();
builder.Services.InyectionAddCustomSwaggerGen();
builder.Services.InyectionAddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();
app.Run();
