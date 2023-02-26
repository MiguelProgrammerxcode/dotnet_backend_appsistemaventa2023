using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios.Contrato
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> GetAsync(Expression<Func<TModel, bool>> filtro);
        Task<TModel> CreateAsync(TModel modelo);
        Task<bool> UpdateAsync(TModel modelo);
        Task<bool> DeleteAsync(TModel modelo);
        Task<IQueryable<TModel>> GetAllAsync(Expression<Func<TModel, bool>>? filtro = null);
    }
}
