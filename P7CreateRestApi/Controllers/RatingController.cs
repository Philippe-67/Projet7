using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {

        private readonly IRatingRepository _ratingRepository;
        public RatingController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
       

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, RH, User")]

        public async Task<IActionResult> Get(int id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);
            if (rating == null)
                return NotFound();
            return Ok(rating);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, RH")]
        //[Route("validate")]
        public async Task<IActionResult> Post([FromBody] Rating rating)
        {
            {
                // TODO: check data valid and save to db, after saving return Rating list
                await _ratingRepository.AddAsync(rating);
                return CreatedAtAction(nameof(Get), new { id = rating.Id }, rating);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        // [Route("update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Rating rating)
        {
            if (id != rating.Id) // TODO: get Rating by Id and to model then show to the form
                return BadRequest();
            await _ratingRepository.UpdateAsync(rating);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        // [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ratingRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}