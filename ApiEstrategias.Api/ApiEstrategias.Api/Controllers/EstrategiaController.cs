using ApiEstrategias.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEstrategias.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstrategiaController : ControllerBase
    {
        public EstrategiaController(IEstrategiaService estrategiaService)
        {
            EstrategiaService = estrategiaService;
        }

        public IEstrategiaService EstrategiaService { get; }

        [Route("generarEstrategia/{CantidaMaximaVueltas}/{Usuario}/{IdPiloto}")]
        [HttpGet]
        public async Task<IActionResult> GetEstrategias(int CantidaMaximaVueltas, string Usuario, long IdPiloto)
        {
            try
            {
                var ResEstrategiasGeneradas = await EstrategiaService.GenerarEstrategias(CantidaMaximaVueltas, IdPiloto, Usuario);
                return Ok(ResEstrategiasGeneradas);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
