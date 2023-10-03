
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurvePointController : ControllerBase
    {
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointController(ICurvePointRepository curvePointRepository)
        {
            _curvePointRepository = curvePointRepository;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var curvePoints = await _curvePointRepository.GetAllAsync();
        //    return Ok(curvePoints);
        //}

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, RH, User")]
        public async Task<IActionResult> Get(int id)
        {
            var curvePoint = await _curvePointRepository.GetByIdAsync(id);
            if (curvePoint == null)
                return NotFound();

            return Ok(curvePoint);
        }

        [HttpPost]

        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Post([FromBody] CurvePoint curvePoint)
        {
            await _curvePointRepository.AddAsync(curvePoint);
            return CreatedAtAction(nameof(Get), new { id = curvePoint.Id }, curvePoint);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Put(int id, [FromBody] CurvePoint curvePoint)
        {
            if (id != curvePoint.Id)
                return BadRequest();

            await _curvePointRepository.UpdateAsync(curvePoint);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Delete(int id)
        {
            await _curvePointRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
