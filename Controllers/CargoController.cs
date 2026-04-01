using GestaoPatrimonio.Applications.Services;
using GestaoPatrimonio.DTOs.BairroDto;
using GestaoPatrimonio.DTOs.CargoDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;

        public CargoController(CargoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarCargoDto>> Listar()
        {
            List<ListarCargoDto> cargosDto = _service.Listar();
            return Ok(cargosDto);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCargoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarCargoDto cargoDto = _service.BuscarPorId(id);
                return Ok(cargoDto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCargoDto cargoDto)
        {
            try
            {
                _service.Adicionar(cargoDto);
                return StatusCode(202, cargoDto);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        public ActionResult Atualizar(CriarCargoDto criarCargoDto, Guid id)
        {
            try
            {
                _service.Atualizar(criarCargoDto, id);
                return StatusCode(204, criarCargoDto);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
