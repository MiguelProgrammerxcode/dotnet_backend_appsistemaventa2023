using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> rolRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = rolRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDto>> Lista()
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.GetAllAsync();
                var listaUsuarios = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();
                return _mapper.Map<List<UsuarioDto>>(listaUsuarios);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SessionDto> ValidarCredenciales(string? correo, string? clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.GetAllAsync(u => u.Correo!.Equals(correo) &&
                                                                              u.Clave!.Equals(clave));
                //
                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");
                //
                Usuario responseUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<SessionDto>(responseUsuario);
            }
            catch
            {
                throw;
            }
        }

        public async Task<UsuarioDto> Crear(UsuarioDto modelo)
        {
            try
            {
                var usuarioCreado = await _usuarioRepositorio.CreateAsync(_mapper.Map<Usuario>(modelo));
                //
                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear");
                //
                var query = await _usuarioRepositorio.GetAllAsync(u => u.IdUsuario == usuarioCreado.IdUsuario);
                //
                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();
                //
                return _mapper.Map<UsuarioDto>(usuarioCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDto modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuario>(modelo);
                //
                var usuarioEncontrado = await _usuarioRepositorio.
                                              GetAsync(u => u.IdUsuario == usuarioModelo.IdUsuario);
                //
                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");
                //
                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo         = usuarioModelo.Correo;
                usuarioEncontrado.IdRol          = usuarioModelo.IdRol;
                usuarioEncontrado.Clave          = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo       = usuarioModelo.EsActivo;
                //
                var respuesta = await _usuarioRepositorio.UpdateAsync(usuarioEncontrado);
                //
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");
                //
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepositorio.GetAsync(u => u.IdUsuario == id);
                //
                if (usuarioEncontrado is null)
                    throw new TaskCanceledException("El usuario no existe");
                //
                var respuesta = await _usuarioRepositorio.DeleteAsync(usuarioEncontrado);
                //
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar el usuario");
                //
                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
