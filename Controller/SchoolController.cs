using Microsoft.AspNetCore.Mvc;
using SMS.Entities;
using SMS.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IEnumerable<School>> GetSchools()
        {
            return await _unitOfWork.Schools.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            var school = await _unitOfWork.Schools.GetByIdAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            return school;
        }

        [HttpPost]
        public async Task<ActionResult<School>> CreateSchool(School school)
        {
            await _unitOfWork.Schools.AddAsync(school);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, school);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, School school)
        {
            if (id != school.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Schools.Update(school);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

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