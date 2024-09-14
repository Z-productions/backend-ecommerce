namespace ecommerce.DAL.Repository.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(object id); // Obtener por ID
        Task<IEnumerable<T>> GetAllAsync(); // Obtener todos los elementos
        Task<IEnumerable<T>> GetByPropertyAsync(string propertyName, object value); // Obtener por propiedad específica
        Task<T> AddAsync(T entity); // Agregar entidad
        Task<bool?> UpdateAsync(T entity); // Actualizar entidad
        Task<bool?> DeleteAsync(T entity); // Eliminar entidad
    }
}
