namespace SistemaVenta.Model;

public sealed class Menu
{
    public int IdMenu { get; set; }

    public string? Nombre { get; set; }

    public string? Icono { get; set; }

    public string? Url { get; set; }

    public IEnumerable<MenuRol> MenuRols { get; } = new List<MenuRol>();
}
