using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Controllers
{
//    [ApiController]
//    [Route("[controller]")]
//    public class RuleNameController : ControllerBase
//    {
//        // TODO: Inject RuleName service

//        [HttpGet]
//        [Route("list")]
//        public IActionResult Home()
//        {
//            // TODO: find all RuleName, add to model
//            return Ok();
//        }

//        [HttpGet]
//        [Route("add")]
//        public IActionResult AddRuleName([FromBody]RuleName trade)
//        {
//            return Ok();
//        }

//        [HttpGet]
//        [Route("validate")]
//        public IActionResult Validate([FromBody]RuleName trade)
//        {
//            // TODO: check data valid and save to db, after saving return RuleName list
//            return Ok();
//        }

//        [HttpGet]
//        [Route("update/{id}")]
//        public IActionResult ShowUpdateForm(int id)
//        {
//            // TODO: get RuleName by Id and to model then show to the form
//            return Ok();
//        }

//        [HttpPost]
//        [Route("update/{id}")]
//        public IActionResult UpdateRuleName(int id, [FromBody] RuleName rating)
//        {
//            // TODO: check required fields, if valid call service to update RuleName and return RuleName list
//            return Ok();
//        }

//        [HttpDelete]
//        [Route("{id}")]
//        public IActionResult DeleteRuleName(int id)
//        {
//            // TODO: Find RuleName by Id and delete the RuleName, return to Rule list
//            return Ok();
//        }
//    }
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

//namespace P7CreateRestApi.Controllers
//{
    [ApiController]
    [Route("[controller]")]
    public class RuleNameController : ControllerBase
    {
        private readonly IRuleNameRepository _ruleNameRepository;

        public RuleNameController(IRuleNameRepository ruleNameRepository)
        {
            _ruleNameRepository = ruleNameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ruleNames = await _ruleNameRepository.GetAllAsync();
            return Ok(ruleNames);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ruleName = await _ruleNameRepository.GetByIdAsync(id);
            if (ruleName == null)
                return NotFound();

            return Ok(ruleName);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RuleName ruleName)
        {
            await _ruleNameRepository.AddAsync(ruleName);
            return CreatedAtAction(nameof(Get), new { id = ruleName.Id }, ruleName);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] RuleName ruleName)
        {
            if (id != ruleName.Id)
                return BadRequest();

            await _ruleNameRepository.UpdateAsync(ruleName);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ruleNameRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

