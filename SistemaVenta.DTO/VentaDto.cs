namespace SistemaVenta.DTO
{
    public sealed class VentaDto
    {
        public int IdVenta { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? TipoPago { get; set; }
        public string? TotalTexto { get; set; }
        public string? FechaRegistro { get; set; }
        public ICollection<DetalleVentaDto>? DetalleVenta { get; set; }
    }
}
