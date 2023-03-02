namespace SistemaVenta.Model;

public sealed class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public Producto? IdProductoNavigation { get; set; }

    public Venta? IdVentaNavigation { get; set; }
}
