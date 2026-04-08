using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestaoPatrimonio.Applications.Services;
using GestaoPatrimonio.DTOs.AreaDto;
using GestaoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _service;

        public AreaController(AreaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult <List<ListarAreaDto>> Listar()
        { 
            List<ListarAreaDto> areas = _service.Listar(); 
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarAreaDto> BuscarPorId(Guid id)
        {
            try
            { 
            ListarAreaDto area = _service.BuscarPorId(id);
            return Ok(area);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarAreaDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarAreaDto dto)
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
