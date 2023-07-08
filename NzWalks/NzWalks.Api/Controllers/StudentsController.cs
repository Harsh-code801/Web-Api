using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //localhost:portnumber/api/Students
    public class StudentsController : ControllerBase
    {
        [HttpGet()]
        // GET: localhost:portnumber/api/Students
        public IActionResult GetAllStudents()
        {
            string[] students = new string[] { "Harsh", "Jeet", "Mahesh", "Brijesh" };
            return Ok(students);
        }
    }
}
