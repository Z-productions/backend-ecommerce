using ecommerce.MODEL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
