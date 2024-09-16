using System.Linq.Expressions;

namespace ecommerce.DAL.Repository.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id); // Obtener por ID
        Task<IEnumerable<T>> GetAllAsync(); // Obtener todos los elementos
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate); // Búsqueda personalizada usando expresión
        Task<T> AddAsync(T entity); // Agregar entidad
        Task<bool?> UpdateAsync(T entity); // Actualizar entidad
        Task<bool?> DeleteAsync(T entity); // Eliminar entidad
    }
}
