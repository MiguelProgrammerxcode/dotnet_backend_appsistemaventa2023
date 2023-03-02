namespace SistemaVenta.Model;

public sealed class Venta
{
    public int IdVenta { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? TipoPago { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public ICollection<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();
}
