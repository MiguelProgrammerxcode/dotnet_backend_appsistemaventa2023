namespace SistemaVenta.Model;

public sealed class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public int? IdCategoria { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public IEnumerable<DetalleVenta> DetalleVenta { get; } = new List<DetalleVenta>();

    public Categoria? IdCategoriaNavigation { get; set; }
}
