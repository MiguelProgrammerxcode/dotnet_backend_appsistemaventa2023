namespace SistemaVenta.Model;

public sealed class Rol
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public IEnumerable<MenuRol> MenuRols { get; } = new List<MenuRol>();

    public IEnumerable<Usuario> Usuarios { get; } = new List<Usuario>();
}
