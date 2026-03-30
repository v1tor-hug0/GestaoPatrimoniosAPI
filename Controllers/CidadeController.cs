using GestaoPatrimonio.Applications.Services;
using GestaoPatrimonio.DTOs.CidadeDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _service;

        public CidadeController (CidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        [HttpGet]
        public ActionResult<List<ListarCidadeDto>> Listar()
        {
            List<ListarCidadeDto> cidades = _service.Listar();
            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCidadeDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarCidadeDto cidade = _service.BuscarPorId(id);
                return Ok(cidade);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCidadeDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarCidadeDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
