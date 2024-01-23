using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.Dto;
using Konefeld.Kopiec.VodkaApp.UI.WEB.Models.FilterObjects;
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

        [HttpPost("filter")]
        public IActionResult Filter([FromBody] VodkaFilter filter)
        {
            return Ok(_vodkaService.GetFilteredVodkas(filter));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_vodkaService.GetVodka(id));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] VodkaDto newVodka)
        {
            var validationResult = _vodkaService.Validate(newVodka);

            if (!validationResult.IsSuccess)
                return BadRequest(validationResult.Message);

            return Ok(_vodkaService.CreateVodka(newVodka));
        }

        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] VodkaDto updatedVodka)
        {
            return Ok(_vodkaService.UpdateVodka(id, updatedVodka));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
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
