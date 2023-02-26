using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IVentaService
    {
        Task<VentaDto> Registrar(VentaDto modelo);
        Task<List<VentaDto>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);
        Task<List<ReporteDto>> Reporte(string fechaInicio, string fechaFin);
    }
}
