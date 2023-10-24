using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurvePointController : ControllerBase
    {
        private readonly ICurvePointRepository _curvePointRepository;
        private readonly ILogger<CurvePointController> _logger;

        public CurvePointController(ILogger<CurvePointController> logger, ICurvePointRepository curvePointRepository)
        {
            _logger = logger;
            _curvePointRepository = curvePointRepository;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, RH, User")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"R�cup�ration du point de courbe avec l'ID : {id}");

            var curvePoint = await _curvePointRepository.GetByIdAsync(id);
            if (curvePoint == null)
            {
                _logger.LogWarning($"Point de courbe avec l'ID {id} non trouv�");
                return NotFound();
            }

            _logger.LogInformation($"Point de courbe avec l'ID {id} r�cup�r� avec succ�s");
            return Ok(curvePoint);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Post([FromBody] CurvePoint curvePoint)
        {
            _logger.LogInformation("Ajout d'un nouveau point de courbe");

            await _curvePointRepository.AddAsync(curvePoint);

            _logger.LogInformation($"Point de courbe ajout� avec succ�s. ID du point de courbe : {curvePoint.Id}");

            return CreatedAtAction(nameof(Get), new { id = curvePoint.Id }, curvePoint);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Put(int id, [FromBody] CurvePoint curvePoint)
        {
            _logger.LogInformation($"Mise � jour du point de courbe avec l'ID : {id}");

            if (id != curvePoint.Id)
            {
                _logger.LogError("Incompatibilit� dans les ID de point de courbe. Requ�te incorrecte.");
                return BadRequest();
            }

            await _curvePointRepository.UpdateAsync(curvePoint);

            _logger.LogInformation($"Point de courbe avec l'ID {id} mis � jour avec succ�s");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Suppression du point de courbe avec l'ID : {id}");

            await _curvePointRepository.DeleteAsync(id);

            _logger.LogInformation($"Point de courbe avec l'ID {id} supprim� avec succ�s");
            return NoContent();
        }
    }
}
