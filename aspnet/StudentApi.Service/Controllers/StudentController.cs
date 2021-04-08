using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Repository;

namespace StudentApi.Controllers 
{
    [ApiController]
    [Route("/rest/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("test")]
        [Produces("application/json")]
        public IActionResult Test()
        {
            var error = new ErrorModel("Hi", "I'm working");
            return Ok(error);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Student.SelectAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var student = (await _unitOfWork.Student.SelectAsync(e => e.EntityID == id)).FirstOrDefault();

                return Ok(student);
            }
            catch(KeyNotFoundException e)
            {
                System.Console.WriteLine("Error: " + e);
                var error = new ErrorModel("Student Get Error", e.Message);
                return NotFound(error);
            }
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string name)
        {
            var students = await _unitOfWork.Student.SelectAsync(
                e => (e.FirstName == name) || (e.LastName == name)
            );
            return Ok(students);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                Student student = (await _unitOfWork.Student.SelectAsync(e => e.EntityID == id)).FirstOrDefault();

                await _unitOfWork.Student.DeleteAsync(student.EntityID);
                await _unitOfWork.CommitAsync();

                return Ok();
            }
            catch(KeyNotFoundException e)
            {
                System.Console.WriteLine("Error: " + e);
                var error = new ErrorModel("Student Delete Error", e.Message);
                return NotFound(error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Student student)
        {
            await _unitOfWork.Student.AddAsync(student);
            await _unitOfWork.CommitAsync();

            return Accepted(student);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Student student)
        {
            try
            {
                var newStudent = (await _unitOfWork.Student
                    .SelectAsync(e => e.EntityID == student.EntityID)).FirstOrDefault();
                _unitOfWork.Student.Update(newStudent);
                await _unitOfWork.CommitAsync();

                return Accepted(student);
            }
            catch(KeyNotFoundException e)
            {
                System.Console.WriteLine("Error: " + e);
                var error = new ErrorModel("Student Put Error", e.Message);
                return NotFound(error);
            }
        }
    }
}