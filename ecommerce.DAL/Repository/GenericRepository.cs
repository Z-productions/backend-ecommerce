using ecommerce.DAL.Repository.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

                // Verificar si se encontró la entidad
                if (entity == null)
                {
                    throw new KeyNotFoundException($"No se encontró la entidad con ID: {id}");
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la entidad por ID: {ex.Message}", ex);
            }
        }

        // Obtener por propiedad por propiedad
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await ecommerceContext.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar las entidades: {ex.Message}", ex);
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
