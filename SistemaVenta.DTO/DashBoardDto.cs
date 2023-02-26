namespace SistemaVenta.DTO
{
    public class DashBoardDto
    {
        public int? TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public int? TotalProductos { get; set; }
        public List<VentasSemanaDto>? VentasUltimaSemana { get; set; }
    }
}
