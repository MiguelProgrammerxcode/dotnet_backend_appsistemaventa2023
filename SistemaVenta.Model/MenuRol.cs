namespace SistemaVenta.Model;

public sealed class MenuRol
{
    public int IdMenuRol { get; set; }

    public int? IdMenu { get; set; }

    public int? IdRol { get; set; }

    public Menu? IdMenuNavigation { get; set; }

    public Rol? IdRolNavigation { get; set; }
}
