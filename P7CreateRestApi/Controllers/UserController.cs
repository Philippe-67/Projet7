//using P7CreateRestApi.Domain;
//using P7CreateRestApi.Repositories;
//using Microsoft.AspNetCore.Mvc;

//namespace P7CreateRestApi.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class UserController : ControllerBase
//    {
//        private UserRepository _userRepository;

//        public UserController(UserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        [HttpGet]
//        [Route("list")]
//        public IActionResult Home()
//        {
//            return Ok();
//        }

//        [HttpGet]
//        [Route("add")]
//        public IActionResult AddUser([FromBody]User user)
//        {
//            return Ok();
//        }

//        [HttpGet]
//        [Route("validate")]
//        public IActionResult Validate([FromBody]User user)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest();
//            }

//           _userRepository.Add(user);

//            return Ok();
//        }

//        [HttpGet]
//        [Route("update/{id}")]
//        public IActionResult ShowUpdateForm(int id)
//        {
//            User user = _userRepository.FindById(id);

//            if (user == null)
//                throw new ArgumentException("Invalid user Id:" + id);

//            return Ok();
//        }

//        [HttpPost]
//        [Route("update/{id}")]
//        public IActionResult UpdateUser(int id, [FromBody] User user)
//        {
//            // TODO: check required fields, if valid call service to update Trade and return Trade list
//            return Ok();
//        }

//        [HttpDelete]
//        [Route("{id}")]
//        public IActionResult DeleteUser(int id)
//        {
//            User user = _userRepository.FindById(id);

//            if (user == null)
//                throw new ArgumentException("Invalid user Id:" + id);

//            return Ok();
//        }

//        [HttpGet]
//        [Route("/secure/article-details")]
//        public async Task<ActionResult<List<User>>> GetAllUserArticles()
//        {
//            return Ok();
//        }
//    }
//}
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //[HttpGet]
        //[Route("list")]
        //public async Task<IActionResult> Home()
        //{
        //    var users = await _userRepository.GetAllUsersAsync();
        //    return Ok(users);
        //}

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await _userRepository.AddUserAsync(user);
            return Ok();
        }

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userRepository.AddUserAsync(user);
            return Ok();
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> ShowUpdateForm(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            // Retournez votre vue ou effectuez d'autres opérations nécessaires
            return Ok(user);
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            // TODO: check required fields, if valid call service to update User and return User list
            await _userRepository.UpdateUserAsync(user);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userRepository.DeleteUserAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("/secure/article-details")]
        public async Task<ActionResult<List<User>>> GetAllUserArticles()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
