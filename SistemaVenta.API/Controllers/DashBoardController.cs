using Microsoft.AspNetCore.Mvc;

using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.API.Utilidad;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashBoardService;

        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashBoardDto>();
            //
            try
            {
                rsp.Status = true;
                rsp.Value  = await _dashBoardService.Resumen();
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
