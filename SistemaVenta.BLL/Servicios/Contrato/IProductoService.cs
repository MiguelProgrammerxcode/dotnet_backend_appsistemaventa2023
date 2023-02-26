using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> Lista();
        Task<ProductoDto> Crear(ProductoDto modelo);
        Task<bool> Editar(ProductoDto modelo);
        Task<bool> Eliminar(int id);
    }
}
