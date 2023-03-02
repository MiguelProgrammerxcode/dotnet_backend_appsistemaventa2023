using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbventaContext;

        public VentaRepository(DbventaContext dbventaContext) : base(dbventaContext)
        {
            _dbventaContext = dbventaContext;
        }

        public async Task<Venta> RegistrarAsync(Venta modelo)
        {
            Venta ventaGenerada;
            await using var transaction = await _dbventaContext.Database.BeginTransactionAsync();
            try
            {
                foreach (var dv in modelo.DetalleVenta)
                {
                    var productoEncontrado = _dbventaContext.Productos!.First(p => p.IdProducto == dv.IdProducto);
                    productoEncontrado.Stock -= dv.Cantidad;
                    _dbventaContext.Productos!.Update(productoEncontrado);
                }
                await _dbventaContext.SaveChangesAsync();

                var correlativo = _dbventaContext.NumeroDocumentos!.First();
                correlativo.UltimoNumero++;
                correlativo.FechaRegistro = DateTime.Now;
                _dbventaContext.NumeroDocumentos!.Update(correlativo);
                await _dbventaContext.SaveChangesAsync();

                const int cantidadDigitos = 4;
                var ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos));
                var numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                // 00001
                numeroVenta = numeroVenta.Substring(numeroVenta.Length - cantidadDigitos, cantidadDigitos);
                // 0001
                modelo.NumeroDocumento = numeroVenta;

                await _dbventaContext.Venta!.AddAsync(modelo);
                await _dbventaContext.SaveChangesAsync();

                ventaGenerada = modelo;
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return ventaGenerada;
        }
    }
}