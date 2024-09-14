using ecommerce.DAL.Repository.Contrato;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EcommerceContext ecommerceContext;

        public GenericRepository(EcommerceContext ecommerceContext)
        {
            this.ecommerceContext = ecommerceContext;
        }

        // Agregar entidad
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await ecommerceContext.Set<T>().AddAsync(entity);
                await ecommerceContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al agregar la entidad: {ex.Message}", ex);
            }
        }

        // Eliminar entidad
        public async Task<bool?> DeleteAsync(T entity)
        {
            try
            {
                ecommerceContext.Set<T>().Remove(entity);
                await ecommerceContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la entidad: {ex.Message}", ex);
            }
        }

        // Obtener todos los elementos
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await ecommerceContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las entidades: {ex.Message}", ex);
            }
        }

        // Obtener por ID
        public async Task<T> GetByIdAsync(object id)
        {
            try
            {
                var entity = await ecommerceContext.Set<T>().FindAsync(id);

                // Devuelve un nuevo objeto de tipo T si la entidad es nula
                return entity ?? Activator.CreateInstance<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la entidad por ID: {ex.Message}", ex);
            }
        }

        // Obtener por propiedad
        public async Task<IEnumerable<T>> GetByPropertyAsync(string propertyName, object value)
        {
            try
            {
                // Obtener la propiedad a través de reflexión
                var propertyInfo = typeof(T).GetProperty(propertyName);

                if (propertyInfo == null)
                {
                    throw new ArgumentException($"La propiedad '{propertyName}' no existe en el tipo '{typeof(T).Name}'");
                }

                // Convertir la lista a memoria 
                var allEntity = await ecommerceContext.Set<T>().ToListAsync();

                // Filtrar usando LINQ
                return allEntity.Where(Entity =>
                {
                    var propety = propertyInfo.GetValue(Entity);
                    return propety != null && propety.Equals(value);
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener entidades por la propiedad '{propertyName}': {ex.Message}", ex);
            }
        }

        // Actualizar entidad
        public async Task<bool?> UpdateAsync(T entity)
        {
            try
            {
                ecommerceContext.Set<T>().Update(entity);
                await ecommerceContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la entidad: {ex.Message}", ex);
            }
        }
    }
}
