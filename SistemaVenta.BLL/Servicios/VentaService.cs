using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using System.Globalization;

namespace SistemaVenta.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(
            IVentaRepository ventaRepositorio, 
            IGenericRepository<DetalleVenta> detalleVentaRepositorio, 
            IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDto> Registrar(VentaDto modelo)
        {
            var ventaGenerada = await _ventaRepositorio.RegistrarAsync(_mapper.Map<Venta>(modelo));
            //
            if (ventaGenerada is null)
                throw new TaskCanceledException("No se pudo registrar la venta");
            //
            return _mapper.Map<VentaDto>(ventaGenerada);
        }

        public async Task<List<VentaDto>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            var query = await _ventaRepositorio.GetAllAsync();
            List<Venta> listaResultado;
            //
            if (buscarPor.Equals("fecha"))
            {
                var fecInicio = DateTime.ParseExact(fechaInicio, "dd/mm/yyyy", new CultureInfo("es-PE"));
                var fecFin = DateTime.ParseExact(fechaFin, "dd/mm/yyyy", new CultureInfo("es-PE"));
                //
                listaResultado = await query.Where(v => v.FechaRegistro!.Value >= fecInicio.Date &&
                                                        v.FechaRegistro!.Value <= fecFin.Date)
                    .Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
            }
            else
            {
                listaResultado = await query.Where(v => v.NumeroDocumento!.Equals(numeroVenta))
                    .Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();
            }

            //
            return _mapper.Map<List<VentaDto>>(listaResultado);
        }

        public async Task<List<ReporteDto>> Reporte(string fechaInicio, string fechaFin)
        {
            var query = await _detalleVentaRepositorio.GetAllAsync();
            List<DetalleVenta> listaResultado;
            //
            var fecInicio = DateTime.ParseExact(fechaInicio, "dd/mm/yyyy", new CultureInfo("es-PE"));
            var fecFin = DateTime.ParseExact(fechaFin, "dd/mm/yyyy", new CultureInfo("es-PE"));
            //
            listaResultado = await query
                .Include(p => p.IdProductoNavigation)
                .Include(v => v.IdVentaNavigation)
                .Where(dv =>
                    dv.IdVentaNavigation!.FechaRegistro!.Value.Date >= fecInicio.Date &&
                    dv.IdVentaNavigation!.FechaRegistro!.Value.Date <= fecFin.Date)
                .ToListAsync();
            //
            return _mapper.Map<List<ReporteDto>>(listaResultado);
        }
    }
}
