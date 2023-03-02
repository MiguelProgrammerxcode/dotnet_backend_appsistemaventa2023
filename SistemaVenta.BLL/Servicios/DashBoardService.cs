using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using System.Globalization;

namespace SistemaVenta.BLL.Servicios
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<Producto> _productoRepositorio;

        public DashBoardService(
            IVentaRepository ventaRepositorio, 
            IGenericRepository<Producto> productoRepositorio
            )
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;
        }

        private static IQueryable<Venta> RetornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            var ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
            ultimaFecha = ultimaFecha!.Value.AddDays(restarCantidadDias);
            return tablaVenta.Where(v => v.FechaRegistro!.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            var total = 0;
            var ventaQuery = await _ventaRepositorio.GetAllAsync();
            if (!ventaQuery.Any()) return total;
            var tablaVenta = RetornarVentas(ventaQuery, -7);
            total = tablaVenta.Count();
            //
            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            var ventaQuery = await _ventaRepositorio.GetAllAsync();
            //
            if (!ventaQuery.Any()) return Convert.ToString(resultado, new CultureInfo("es-PE"));
            var tablaVenta = RetornarVentas(ventaQuery, -7);
            resultado = tablaVenta.Select(v => v.Total).Sum(v => v!.Value);
            //
            return Convert.ToString(resultado, new CultureInfo("es-PE"));
        }

        private async Task<int> TotalProductos()
        {
            var productoQuery = await _productoRepositorio.GetAllAsync();
            var total = productoQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            var resultado = new Dictionary<string, int>();
            var ventaQuery = await _ventaRepositorio.GetAllAsync();
            //
            if (!ventaQuery.Any()) return resultado;
            var tablaVenta = RetornarVentas(ventaQuery, -7);
            resultado = tablaVenta.GroupBy(v => v.FechaRegistro!.Value.Date).OrderBy(g => g.Key)
                .Select(dv => new { fecha = dv.Key.ToString("dd/mm/yyyy"), total = dv.Count() })
                .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);

            return resultado;
        }

        public async Task<DashBoardDto> Resumen()
        {
            var dashBoardDto = new DashBoardDto
            {
                TotalVentas = await TotalVentasUltimaSemana(),
                TotalIngresos = await TotalIngresosUltimaSemana(),
                TotalProductos = await TotalProductos()
            };

            var listaVentaSemana = (await VentasUltimaSemana()).Select(item => new VentasSemanaDto() { Fecha = item.Key, Total = item.Value }).ToList();

            dashBoardDto.VentasUltimaSemana = listaVentaSemana;
            //
            return dashBoardDto;
        }
    }
}
