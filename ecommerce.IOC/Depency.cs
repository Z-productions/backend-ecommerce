using ecommerce.BLL.Servicios;
using ecommerce.BLL.Servicios.Contrato;
using ecommerce.DAL;
using ecommerce.DAL.Repository;
using ecommerce.DAL.Repository.Contrato;
//using ecommerce.MODEL;
using ecommerce.UTILIY;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            // Registrar AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
