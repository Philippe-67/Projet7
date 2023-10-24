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
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ILogger<RatingController> logger, IRatingRepository ratingRepository)
        {
            _logger = logger;
            _ratingRepository = ratingRepository;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, RH, User")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"R�cup�ration de la note avec l'ID : {id}");

            var rating = await _ratingRepository.GetByIdAsync(id);
            if (rating == null)
            {
                _logger.LogWarning($"Note avec l'ID {id} non trouv�e");
                return NotFound();
            }

            _logger.LogInformation($"Note avec l'ID {id} r�cup�r�e avec succ�s");
            return Ok(rating);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Post([FromBody] Rating rating)
        {
            _logger.LogInformation("Ajout d'une nouvelle note");

            await _ratingRepository.AddAsync(rating);

            _logger.LogInformation($"Note ajout�e avec succ�s. ID de la note : {rating.Id}");

            return CreatedAtAction(nameof(Get), new { id = rating.Id }, rating);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Put(int id, [FromBody] Rating rating)
        {
            _logger.LogInformation($"Mise � jour de la note avec l'ID : {id}");

            if (id != rating.Id)
            {
                _logger.LogError("Incompatibilit� dans les ID de note. Requ�te incorrecte.");
                return BadRequest();
            }

            await _ratingRepository.UpdateAsync(rating);

            _logger.LogInformation($"Note avec l'ID {id} mise � jour avec succ�s");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Suppression de la note avec l'ID : {id}");

            await _ratingRepository.DeleteAsync(id);

            _logger.LogInformation($"Note avec l'ID {id} supprim�e avec succ�s");
            return NoContent();
        }
    }
}
