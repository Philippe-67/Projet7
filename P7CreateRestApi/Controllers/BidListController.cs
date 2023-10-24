using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListRepository _bidListRepository;
        private readonly ILogger<BidListController> _logger;

        public BidListController(ILogger<BidListController> logger, IBidListRepository bidListRepository)
        {
            _logger = logger;
            _bidListRepository = bidListRepository;
        }

        [HttpGet("liste des utilisateurs/{id}")]
        [Authorize(Roles = "Admin, RH, User")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Récupération de la liste d'offres avec l'ID : {id}");

            var bidList = await _bidListRepository.GetByIdAsync(id);
            if (bidList == null)
            {
                _logger.LogWarning($"Liste d'offres avec l'ID {id} non trouvée");
                return NotFound();
            }

            _logger.LogInformation($"Liste d'offres avec l'ID {id} récupérée avec succès");
            return Ok(bidList);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Post([FromBody] BidList bidList)
        {
            _logger.LogInformation("Ajout d'une nouvelle liste d'offres");

            await _bidListRepository.AddAsync(bidList);

            _logger.LogInformation($"Liste d'offres ajoutée avec succès. ID de la liste d'offres : {bidList.BidListId}");

            return CreatedAtAction(nameof(Get), new { id = bidList.BidListId }, bidList);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Put(int id, [FromBody] BidList bidList)
        {
            _logger.LogInformation($"Mise à jour de la liste d'offres avec l'ID : {id}");

            if (id != bidList.BidListId)
            {
                _logger.LogError("Incompatibilité dans les ID de liste d'offres. Requête incorrecte.");
                return BadRequest();
            }

            await _bidListRepository.UpdateAsync(bidList);

            _logger.LogInformation($"Liste d'offres avec l'ID {id} mise à jour avec succès");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Tentative d'élimination à {DateTime.Now} de la liste d'offres avec l'ID {id}");

            await _bidListRepository.DeleteAsync(id);

            _logger.LogInformation($"Liste d'offres avec l'ID {id} supprimée avec succès");
            return NoContent();
        }
    }
}
