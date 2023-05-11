using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IPrograms _program;
        public HomeController(IPrograms program)
        {
            _program = program;
        }
        [HttpGet]
        
        public ActionResult Index()
        {
            return Ok("Hola");
        }
        [HttpGet("Events")]
        public async Task<ActionResult> Events()
        {
            return Ok(await _program.GetListProgramsAsync());
        }
        //Get Item Program
        [HttpGet("Events/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await _program.CheckProgramAsync(id))
            {
                return Ok(await _program.GetProgramDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Event" });
        } 
    }
}