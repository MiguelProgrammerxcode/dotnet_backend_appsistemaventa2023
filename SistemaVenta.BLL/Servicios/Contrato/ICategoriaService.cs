using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> Lista();
    }
}
