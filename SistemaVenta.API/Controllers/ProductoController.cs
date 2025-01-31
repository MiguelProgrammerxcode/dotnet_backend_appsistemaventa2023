﻿using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ProductoDto>>();
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _productoService.Lista();
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg    = ex.Message;
            }
            //
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ProductoDto producto)
        {
            var rsp = new Response<ProductoDto>();
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _productoService.Crear(producto);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg    = ex.Message;
            }
            //
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ProductoDto producto)
        {
            var rsp = new Response<bool>();
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _productoService.Editar(producto);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Msg    = ex.Message;
            }
            //
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _productoService.Eliminar(id);
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
