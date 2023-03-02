namespace SistemaVenta.Model;

public sealed class Categoria
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public IEnumerable<Producto> Productos { get; } = new List<Producto>();
}
