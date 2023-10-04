using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Mvc;



using P7CreateRestApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeController(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var trades = await _tradeRepository.GetAllAsync();
        //    return Ok(trades);
        //}

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, RH, User")]
        public async Task<IActionResult> Get(int id)
        {
            var trade = await _tradeRepository.GetByIdAsync(id);
            if (trade == null)
                return NotFound();

            return Ok(trade);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Trade trade)
        {
            await _tradeRepository.AddAsync(trade);
            return CreatedAtAction(nameof(Get), new { id = trade.TradeId }, trade);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Put(int id, [FromBody] Trade trade)
        {
            if (id != trade.TradeId)
                return BadRequest();

            await _tradeRepository.UpdateAsync(trade);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tradeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
