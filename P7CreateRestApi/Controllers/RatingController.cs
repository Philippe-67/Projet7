using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Ajout de cet espace de noms pour accéder aux codes d'état HTTP
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status200OK)] // OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Récupération de la note avec l'ID : {id}");

            var rating = await _ratingRepository.GetByIdAsync(id);
            if (rating == null)
            {
                _logger.LogWarning($"Note avec l'ID {id} non trouvée");
                return NotFound();
            }

            _logger.LogInformation($"Note avec l'ID {id} récupérée avec succès");
            return Ok(rating);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, RH")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        public async Task<IActionResult> Post([FromBody] Rating rating)
        {
            _logger.LogInformation("Ajout d'une nouvelle note");
            rating.Id = 0;//////////////////////////////////

            await _ratingRepository.AddAsync(rating);

            _logger.LogInformation($"Note ajoutée avec succès. ID de la note : {rating.Id}");

            return CreatedAtAction(nameof(Get), new { id = rating.Id }, rating);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Success
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        public async Task<IActionResult> Put(int id, [FromBody] Rating rating)
        {
            _logger.LogInformation($"Mise à jour de la note avec l'ID : {id}");

            if (id != rating.Id)
            {
                _logger.LogError("Incompatibilité dans les ID de note. Requête incorrecte.");
                return BadRequest();
            }

            await _ratingRepository.UpdateAsync(rating);

            _logger.LogInformation($"Note avec l'ID {id} mise à jour avec succès");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Suppression de la note avec l'ID : {id}");

            await _ratingRepository.DeleteAsync(id);

            _logger.LogInformation($"Note avec l'ID {id} supprimée avec succès");
            return NoContent();
        }
    }
}
