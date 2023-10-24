using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            _logger.LogInformation($"Ajout de l'utilisateur : {user.Id}");
            await _userRepository.AddUserAsync(user);
            _logger.LogInformation($"Utilisateur ajouté avec succès : {user.Id}");
            return Ok();
        }

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] User user)
        {
            _logger.LogInformation($"Validation de l'utilisateur : {user.Id}");

            if (!ModelState.IsValid)
            {
                _logger.LogError("ModelState invalide");
                return BadRequest();
            }

            await _userRepository.AddUserAsync(user);
            _logger.LogInformation($"Utilisateur validé et ajouté avec succès : {user.Id}");
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation($"Suppression de l'utilisateur avec l'ID : {id}");

            await _userRepository.DeleteUserAsync(id);
            _logger.LogInformation($"Utilisateur supprimé avec succès : {id}");

            return Ok();
        }

        [HttpGet]
        [Route("/secure/article-details")]
        public async Task<ActionResult<List<User>>> GetAllUserArticles()
        {
            _logger.LogInformation("Récupération de tous les articles de l'utilisateur");
            var users = await _userRepository.GetAllUsersAsync();
            _logger.LogInformation($"Récupéré  articles de l'utilisateur");
            return Ok(users);
        }
    }
}
