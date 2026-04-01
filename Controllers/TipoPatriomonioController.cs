using GestaoPatrimonio.Applications.Services;
using GestaoPatrimonio.DTOs.TipoPatrimonioDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPatriomonioController : ControllerBase
    {

        private readonly TipoPatrimonioService _service;

        public TipoPatriomonioController(TipoPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoPatrimonioDto>> Listar()
        {
            return Ok(_service.Listar());
        }
    }
}
