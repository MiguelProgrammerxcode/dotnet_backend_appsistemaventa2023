﻿using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDto venta)
        {
            var rsp = new Response<VentaDto>();
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _ventaService.Registrar(venta);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg    = ex.Message;
            }
            //
            return Ok(rsp);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string? buscarPor, string? numeroVenta,
                                                   string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<VentaDto>>();
            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin    = fechaFin    is null ? "" : fechaFin;
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _ventaService.Historial(buscarPor!, numeroVenta, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg    = ex.Message;
            }
            //
            return Ok(rsp);
        }

        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<ReporteDto>>();
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _ventaService.Reporte(fechaInicio!, fechaFin!);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg    = ex.Message;
            }
            //
            return Ok(rsp);
        }
    }
}
