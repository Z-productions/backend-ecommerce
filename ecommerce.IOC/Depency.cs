using ecommerce.BLL.Servicios;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.BLL.Servicios.Contrato.JWT;
using ecommerce.DAL;
using ecommerce.DAL.Repository;
using ecommerce.DAL.Repository.Contrato;
using ecommerce.UTILIY;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ecommerce.IOC
{
    public static class Depency
    {
        public static void InyectionDependecy(this IServiceCollection services)
        {
            // Obtener las variables del archivo .env
            var server = Environment.GetEnvironmentVariable("DB_SERVER");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            // Crear la cadena de conexión
            var connectionString = $"Server={server};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";

            // Configurar el DbContext
            services.AddDbContext<EcommerceContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Registrar servicios y repositorios
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IBuyerService, BuyerService>();

            // Registrar AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public static void InyectionDependcyJwt(this IServiceCollection services)
        {
            // Leer configuración JWT desde las variables de entorno
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");

            // Verificar si jwtSecret es null o vacío 
            if (string.IsNullOrWhiteSpace(jwtSecret))
            {
                throw new InvalidOperationException("La clave secreta JWT no está configurada.");
            }

            // Configurar JwtSettings
            services.Configure<JwtSettings>(options =>
            {
                options.Secret = jwtSecret;
            });

            // Configurar autenticación JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                            ClockSkew = TimeSpan.Zero // Eliminar tolerancia predeterminada de 5 minutos
                        };
                    });

            services.AddScoped<ITokenService, TokenServices>();
            // Registrar autorización
            services.AddAuthorization();
        }


        public static void InyectionAddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Configuración de Swagger
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mi API",
                    Version = "v1"
                });

                // Configuración de seguridad para JWT
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http, // Usar Http para el tipo de esquema Bearer
                    Scheme = "bearer", // Usar "bearer" en minúsculas
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese el token JWT en el formato: Bearer {token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                      new OpenApiSecurityScheme
                      {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                      },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void InyectionAddCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                       .AllowAnyMethod()
                                       .AllowAnyHeader());
            });
        }
    }
}
