using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize(Roles = "manager")]
    [Route("Events-Manager")]
    [Route("Manager/Events")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        
        private readonly IPrograms _program;
        public ProgramController(IPrograms program)
        {
            _program = program;
        }
        //Get List Program
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _program.GetListProgramsAsync());
        }
        //Get Item Program
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await _program.CheckProgramAsync(id))
            {
                return Ok(await _program.GetProgramDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Event" });
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProgramDto program)
        {
            return (await _program.AddProgramAsync(program))?Ok(new Response { Status = "Success", Message = "Event created successfully!" }):BadRequest(new Response { Status = "Fail", Message = "Event created fail!" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] ProgramInfo program)
        {
            if (await _program.CheckProgramAsync(id))
            {
                if (await _program.UpdateProgramAsync(id,program)){
                    return Ok(new Response { Status = "Success", Message = "Event updated successfully!" });
                }        
            }
            return BadRequest(new Response { Status = "Fail", Message = "Event updated fail!" });         
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _program.CheckProgramAsync(id))
            {
                if (await _program.DeleteProgramAsync(id)){
                    return Ok(new Response { Status = "Success", Message = "Event deleted successfully!" });
                }
            }
            return BadRequest(new Response { Status = "Fail", Message = "Event deleted fail!" });
        }

    }
}