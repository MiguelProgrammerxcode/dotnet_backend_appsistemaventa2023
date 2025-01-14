﻿using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDto>>();
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _categoriaService.Lista();
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
