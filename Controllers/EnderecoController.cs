using GestaoPatrimonio.Applications.Services;
using GestaoPatrimonio.DTOs.BairroDto;
using GestaoPatrimonio.DTOs.EnderecoDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {

        private readonly EnderecoService _service;

        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarBairroDto>> Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public ActionResult<ListarBairroDto> BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_service.BuscarPorId(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarEnderecoDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarEnderecoDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
