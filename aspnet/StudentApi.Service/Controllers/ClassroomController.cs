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
    public class ClassroomController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ClassroomController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Classroom>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Classroom.SelectAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Classroom), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var classroom = (await _unitOfWork.Classroom
                    .SelectAsync(e => e.EntityID == id))
                    .FirstOrDefault();

                return Ok(classroom);
            }
            catch(KeyNotFoundException e)
            {
                System.Console.WriteLine("Error: " + e);
                var error = new ErrorModel("Classroom Get Error", e.Message);
                return NotFound(error);
            }
        }

        [HttpGet("code/{code}")]
        [ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string code)
        {
            var classroom = await _unitOfWork.Classroom.SelectAsync(
                e => (e.ClassCode == code)
            );
            return Ok(classroom);
        }
    }
}
