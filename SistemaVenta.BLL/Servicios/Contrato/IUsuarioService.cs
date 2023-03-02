using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDto>> Lista();
        Task<SessionDto> ValidarCredenciales(string? correo, string? clave);
        Task<UsuarioDto> Crear(UsuarioDto modelo);
        Task<bool> Editar(UsuarioDto modelo);
        Task<bool> Eliminar(int id);
    }
}
