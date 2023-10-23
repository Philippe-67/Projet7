using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly IBidListRepository _bidListRepository;

        public BidListController(IBidListRepository bidListRepository)
        {
            _bidListRepository = bidListRepository;
        }

        [HttpGet("liste des utilisateurs/{id}")]
        [Authorize(Roles = "Admin, RH, User")]
        public async Task<IActionResult> Get(int id)
        {
            var bidList = await _bidListRepository.GetByIdAsync(id);
            if (bidList == null)
                return NotFound();

            return Ok(bidList);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Post([FromBody] BidList bidList)
        {
            await _bidListRepository.AddAsync(bidList);
            return CreatedAtAction(nameof(Get), new { id = bidList.BidListId }, bidList);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Put(int id, [FromBody] BidList bidList)
        {
            if (id != bidList.BidListId)
                return BadRequest();

            await _bidListRepository.UpdateAsync(bidList);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bidListRepository.DeleteAsync(id);
            return NoContent();
        }
    }



}