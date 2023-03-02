using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios
{
    public class GenericRepository<TModelo> : IGenericRepository<TModelo> where TModelo : class
    {
        private readonly DbventaContext _dbContext;

        public GenericRepository(DbventaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TModelo> CreateAsync(TModelo modelo)
        {
            _dbContext.Set<TModelo>().Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }

        public async Task<bool> DeleteAsync(TModelo modelo)
        {
            _dbContext.Set<TModelo>().Remove(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IQueryable<TModelo>> GetAllAsync(Expression<Func<TModelo, bool>>? filtro = null)
        {
            var queryModel = filtro == null ? _dbContext.Set<TModelo>() : _dbContext.Set<TModelo>().Where(filtro);
            return Task.FromResult(queryModel);
        }

        public async Task<TModelo> GetAsync(Expression<Func<TModelo, bool>> filtro)
        {
            var model = (await _dbContext.Set<TModelo>().FirstOrDefaultAsync(filtro))!;
            return model;
        }

        public async Task<bool> UpdateAsync(TModelo modelo)
        {
            _dbContext.Set<TModelo>().Update(modelo);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
