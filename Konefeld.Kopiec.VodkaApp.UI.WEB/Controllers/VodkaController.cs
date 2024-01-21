using Konefeld.Kopiec.VodkaApp.Interfaces;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Services;
using Microsoft.AspNetCore.Mvc;

namespace Konefeld.Kopiec.VodkaApp.UI.WEB.Controllers
{
    [Route("api/vodkas")]
    [ApiController]
    public class VodkaController : ControllerBase
    {
        private readonly IVodkaService _vodkaService;

        public VodkaController(IVodkaService vodkaService)
        {
            _vodkaService = vodkaService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_vodkaService.GetVodkas());
        }

        [HttpPost]
        public IActionResult Filter([FromBody] IVodkaFilter filter)
        {
            return Ok(_vodkaService.GetFilteredVodkas(filter));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_vodkaService.GetVodka(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] IVodkaDto newVodka)
        {
            var validationResult = _vodkaService.Validate(newVodka);

            if (!validationResult.IsSuccess)
                return BadRequest(validationResult.Message);

            return Ok(_vodkaService.CreateVodka(newVodka));
        }

        [HttpPost("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] IVodkaDto updatedVodka)
        {
            return Ok(_vodkaService.UpdateVodka(id, updatedVodka));
        }

        [HttpPost]
        public IActionResult Delete([FromBody] int id)
        {
            return Ok(_vodkaService.DeleteVodka(id));
        }

        [HttpGet("producers")]
        public IActionResult GetProducersData()
        {
            return Ok(_vodkaService.GetProducersData());
        }


    }
}
