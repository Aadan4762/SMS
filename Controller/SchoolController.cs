using Microsoft.AspNetCore.Mvc;
using SMS.Entities;
using SMS.Interface;

namespace SMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchoolController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/School
        [HttpGet]
        public async Task<IEnumerable<School>> GetSchools()
        {
            return await _unitOfWork.Schools.GetAllAsync();
        }

        // GET: api/School/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchoolById(int id)
        {
            var school = await _unitOfWork.Schools.GetByIdAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            return Ok(school);
        }

        // POST: api/School
        [HttpPost]
        public async Task<ActionResult<School>> CreateSchool(School school)
        {
            await _unitOfWork.Schools.AddAsync(school);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetSchoolById), new { id = school.id }, school);
        }

        // PUT: api/School/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, School school)
        {
            if (id != school.id)
            {
                return BadRequest("School ID mismatch");
            }

            var existingSchool = await _unitOfWork.Schools.GetByIdAsync(id);
            if (existingSchool == null)
            {
                return NotFound();
            }

            _unitOfWork.Schools.Update(school);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        // DELETE: api/School/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var school = await _unitOfWork.Schools.GetByIdAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _unitOfWork.Schools.Delete(school);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
