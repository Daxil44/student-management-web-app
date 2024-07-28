using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudenManagementBackEnd.Models;

namespace StudenManagementBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudenContext _context;
        public StudentController(StudenContext studenContext )
        {
            _context = studenContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            return await _context.Students.ToListAsync();
        }


        [HttpGet("{rollnum}")]
        public async Task<ActionResult<Student>> GetStudent(int rollnum)
        {
            if (_context.Students == null )
            {
                return NotFound();
            }
            var student = _context.Students.Find(rollnum);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudentDetail(Student studentDetail)
        {
            //if(ModelState.IsValid)
            if (StudentDetailExists(studentDetail.rollnum))
            {
                return BadRequest("Student with the same Roll Number already exists.");
            }

            _context.Students.Add(studentDetail);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudents", new { Rollnum = studentDetail.rollnum }, studentDetail);
        }
        private bool StudentDetailExists(int rollnum)
        {
            return _context.Students.Any(e => e.rollnum == rollnum);
        }

        [HttpPut("{rollnum}")]

        public async Task<IActionResult> PutStudentDetail(int rollnum, Student StudentDetail)
        {
            if (rollnum != StudentDetail.rollnum)
            {
                return BadRequest();
            }

            _context.Entry(StudentDetail).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDetailExists(rollnum))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpDelete("{rollnum}")]

        public async Task<ActionResult<Student>> DeleteStudentDetail(int rollnum)
        {
            var StudentDetail = await _context.Students.FindAsync(rollnum);
            if (StudentDetail == null)
            {
                return NotFound();
            }

            _context.Students.Remove(StudentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
