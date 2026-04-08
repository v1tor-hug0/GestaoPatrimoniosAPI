using GestaoPatrimonio.Applications.Services;
using GestaoPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {
        private readonly SolicitacaoTransferenciaService _service;

        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarSolicitacaoTransferenciaDto>> Listar()
        {
            List<ListarSolicitacaoTransferenciaDto> solicitacoes = _service.Listar();
            return Ok(solicitacoes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarSolicitacaoTransferenciaDto> BuscarPorId(Guid transferenciaId)
        {
            try
            {
                ListarSolicitacaoTransferenciaDto solicitacoes = _service.BuscarPorId(transferenciaId);
                return Ok(solicitacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
