using Microsoft.AspNetCore.Mvc;
using SMS.Entities;
using SMS.Interface;


namespace SMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            return await _unitOfWork.Teachers.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> CreateTeacher(Teacher teacher)
        {
            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.id }, teacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.id)
            {
                return BadRequest();
            }

            _unitOfWork.Teachers.Update(teacher);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _unitOfWork.Teachers.Delete(teacher);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
