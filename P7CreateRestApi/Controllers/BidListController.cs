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
            _logger.LogInformation($"R�cup�ration de la liste d'offres avec l'ID : {id}");

            var bidList = await _bidListRepository.GetByIdAsync(id);
            if (bidList == null)
            {
                _logger.LogWarning($"Liste d'offres avec l'ID {id} non trouv�e");
                return NotFound();
            }

            _logger.LogInformation($"Liste d'offres avec l'ID {id} r�cup�r�e avec succ�s");
            return Ok(bidList);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Post([FromBody] BidList bidList)
        {
            _logger.LogInformation("Ajout d'une nouvelle liste d'offres");

            await _bidListRepository.AddAsync(bidList);

            _logger.LogInformation($"Liste d'offres ajout�e avec succ�s. ID de la liste d'offres : {bidList.BidListId}");

            return CreatedAtAction(nameof(Get), new { id = bidList.BidListId }, bidList);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Put(int id, [FromBody] BidList bidList)
        {
            _logger.LogInformation($"Mise � jour de la liste d'offres avec l'ID : {id}");

            if (id != bidList.BidListId)
            {
                _logger.LogError("Incompatibilit� dans les ID de liste d'offres. Requ�te incorrecte.");
                return BadRequest();
            }

            await _bidListRepository.UpdateAsync(bidList);

            _logger.LogInformation($"Liste d'offres avec l'ID {id} mise � jour avec succ�s");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Tentative d'�limination � {DateTime.Now} de la liste d'offres avec l'ID {id}");

            await _bidListRepository.DeleteAsync(id);

            _logger.LogInformation($"Liste d'offres avec l'ID {id} supprim�e avec succ�s");
            return NoContent();
        }
    }
}
