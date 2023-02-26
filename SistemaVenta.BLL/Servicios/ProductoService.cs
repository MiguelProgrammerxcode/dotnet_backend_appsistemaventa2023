using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductoDto>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepositorio.GetAllAsync();
                var listaProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();
                return _mapper.Map<List<ProductoDto>>(listaProductos);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoDto> Crear(ProductoDto modelo)
        {
            try
            {
                var productoCreado = await _productoRepositorio.CreateAsync(_mapper.Map<Producto>(modelo));
                //
                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("No se pudo crear");
                //
                return _mapper.Map<ProductoDto>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDto modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _productoRepositorio.GetAsync(u =>
                                                                             u.IdProducto == productoModelo.IdProducto);
                //
                if (productoEncontrado is null)
                    throw new TaskCanceledException("El producto no existe");
                //
                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                var productoModificado = await _productoRepositorio.UpdateAsync(productoEncontrado);
                //
                if (!productoModificado)
                    throw new TaskCanceledException("El producto no se pudo editar");
                //
                return productoModificado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepositorio.GetAsync(p => p.IdProducto == id);
                //
                if (productoEncontrado is null)
                    throw new TaskCanceledException("El producto no existe");
                //
                var productoEliminado = await _productoRepositorio.DeleteAsync(productoEncontrado);
                //
                if (!productoEliminado)
                    throw new TaskCanceledException("El producto no se pudo eliminar");
                //
                return productoEliminado;
            }
            catch
            {
                throw;
            }
        }
    }
}
