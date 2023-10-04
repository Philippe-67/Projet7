using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;

using P7CreateRestApi.Repositories;


[ApiController]
[Route("[controller]")]
public class RuleNameController : ControllerBase
{
    private readonly IRuleNameRepository _ruleNameRepository;

    public RuleNameController(IRuleNameRepository ruleNameRepository)
    {
        _ruleNameRepository = ruleNameRepository;
    }

  

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, RH, User")]
    public async Task<IActionResult> Get(int id)
    {
        var ruleName = await _ruleNameRepository.GetByIdAsync(id);
        if (ruleName == null)
            return NotFound();

        return Ok(ruleName);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, RH")]
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
    [Authorize(Roles = "Admin, RH")]
    public async Task<IActionResult> Delete(int id)
    {
        await _ruleNameRepository.DeleteAsync(id);
        return NoContent();
    }
}


