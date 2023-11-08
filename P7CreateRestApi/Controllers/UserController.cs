using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        _logger.LogInformation($"Ajout de l'utilisateur : {user.Id}");
        await _userRepository.AddUserAsync(user);
        _logger.LogInformation($"Utilisateur ajout� avec succ�s : {user.Id}");
        return Ok();
    }

    [HttpPost("validate")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
    public async Task<IActionResult> Validate([FromBody] User user)
    {
        _logger.LogInformation($"Validation de l'utilisateur : {user.Id}");

        if (!ModelState.IsValid)
        {
            _logger.LogError("ModelState invalide");
            return BadRequest();
        }

        await _userRepository.AddUserAsync(user);
        _logger.LogInformation($"Utilisateur valid� et ajout� avec succ�s : {user.Id}");
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    public async Task<IActionResult> DeleteUser(string id)
    {
        _logger.LogInformation($"Suppression de l'utilisateur avec l'ID : {id}");

        await _userRepository.DeleteUserAsync(id);
        _logger.LogInformation($"Utilisateur supprim� avec succ�s : {id}");

        return Ok();
    }

    [HttpGet("secure/article-details")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    public async Task<ActionResult<List<User>>> GetAllUserArticles()
    {
        _logger.LogInformation("R�cup�ration de tous les articles de l'utilisateur");
        var users = await _userRepository.GetAllUsersAsync();
        _logger.LogInformation("R�cup�r� articles de l'utilisateur");
        return Ok(users);
    }
}
