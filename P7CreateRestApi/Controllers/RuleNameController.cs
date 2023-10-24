using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class RuleNameController : ControllerBase
{
    private readonly IRuleNameRepository _ruleNameRepository;
    private readonly ILogger<RuleNameController> _logger;

    public RuleNameController(ILogger<RuleNameController> logger, IRuleNameRepository ruleNameRepository)
    {
        _logger = logger;
        _ruleNameRepository = ruleNameRepository;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, RH, User")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation($"R�cup�ration du nom de r�gle avec l'ID : {id}");

        var ruleName = await _ruleNameRepository.GetByIdAsync(id);
        if (ruleName == null)
        {
            _logger.LogWarning($"Nom de r�gle avec l'ID {id} non trouv�");
            return NotFound();
        }

        _logger.LogInformation($"Nom de r�gle avec l'ID {id} r�cup�r� avec succ�s");
        return Ok(ruleName);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, RH")]
    public async Task<IActionResult> Post([FromBody] RuleName ruleName)
    {
        _logger.LogInformation("Ajout d'un nouveau nom de r�gle");

        await _ruleNameRepository.AddAsync(ruleName);

        _logger.LogInformation($"Nom de r�gle ajout� avec succ�s. ID du nom de r�gle : {ruleName.Id}");

        return CreatedAtAction(nameof(Get), new { id = ruleName.Id }, ruleName);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] RuleName ruleName)
    {
        _logger.LogInformation($"Mise � jour du nom de r�gle avec l'ID : {id}");

        if (id != ruleName.Id)
        {
            _logger.LogError("Incompatibilit� dans les ID de nom de r�gle. Requ�te incorrecte.");
            return BadRequest();
        }

        await _ruleNameRepository.UpdateAsync(ruleName);

        _logger.LogInformation($"Nom de r�gle avec l'ID {id} mis � jour avec succ�s");
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin, RH")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation($"Suppression du nom de r�gle avec l'ID : {id}");

        await _ruleNameRepository.DeleteAsync(id);

        _logger.LogInformation($"Nom de r�gle avec l'ID {id} supprim� avec succ�s");
        return NoContent();
    }
}
