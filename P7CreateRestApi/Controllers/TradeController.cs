using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Mvc;

//namespace P7CreateRestApi.Controllers
//{
//    [ApiController] 
//    [Route("[controller]")]
//    public class TradeController : ControllerBase
//    {
//        // TODO: Inject Trade service

//        [HttpGet]
//        [Route("list")]
//        public IActionResult Home()
//        {
//            // TODO: find all Trade, add to model
//            return Ok();
//        }

//        [HttpGet]
//        [Route("add")]
//        public IActionResult AddTrade([FromBody]Trade trade)
//        {
//            return Ok();
//        }

//        [HttpGet]
//        [Route("validate")]
//        public IActionResult Validate([FromBody]Trade trade)
//        {
//            // TODO: check data valid and save to db, after saving return Trade list
//            return Ok();
//        }

//        [HttpGet]
//        [Route("update/{id}")]
//        public IActionResult ShowUpdateForm(int id)
//        {
//            // TODO: get Trade by Id and to model then show to the form
//            return Ok();
//        }

//        [HttpPost]
//        [Route("update/{id}")]
//        public IActionResult UpdateTrade(int id, [FromBody] Trade trade)
//        {
//            // TODO: check required fields, if valid call service to update Trade and return Trade list
//            return Ok();
//        }

//        [HttpDelete]
//        [Route("{id}")]
//        public IActionResult DeleteTrade(int id)
//        {
//            // TODO: Find Trade by Id and delete the Trade, return to Trade list
//            return Ok();
//        }
//    }
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trades = await _tradeRepository.GetAllAsync();
            return Ok(trades);
        }

        [HttpGet("{id}")]
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
        public async Task<IActionResult> Put(int id, [FromBody] Trade trade)
        {
            if (id != trade.TradeId)
                return BadRequest();

            await _tradeRepository.UpdateAsync(trade);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tradeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
