using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;

namespace app.Controllers
{
    [Route("Program")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        
        private readonly IPrograms _IProgram;
        private readonly ILocations _ILocation;
        private readonly IArtists _IArtist;
        public ProgramController(IPrograms IProgram,ILocations ILocation,IArtists IArtist)
        {
            _IProgram = IProgram;
            _ILocation = ILocation;
            _IArtist = IArtist;
        }
        //Get List Program
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return new JsonResult(await Task.FromResult(_IProgram.GetListPrograms()));
        }
        //Get Item Program
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await Task.FromResult(_IProgram.CheckProgram(id)))
            {
                var ps = await Task.FromResult(_IProgram.GetProgramDetails(id));
                if (ps == null)
                {
                    return new JsonResult(new{data = "empty"});
                }
                var loca = (await Task.FromResult(_ILocation.CheckLocation(ps.l_id)))?(await Task.FromResult(_ILocation.GetLocationName(ps.l_id))):"";
                return new JsonResult(new{
                    id_chuongtrinh = ps.pid,
                    chuongtrinh_name = ps.name,
                    chuongtrinh_content = ps.content,
                    type_inoff = ps.type_inoff,
                    price = ps.price,
                    typeprogram = ps.type_program,
                    arrange = 3,
                    detail_list = new{
                        time = ps.held_on.ToString("HH:mm:ss"),
                        fdate = ps.create_at.ToString("dd-MM-yyyy"),
                        tdate = ps.held_on.ToString("dd-MM-yyyy"),
                        id_diadiem = ps.l_id,
                        diadiem_name = loca,
                        id_doan = ps.u_id,
                        id_nhom = ps.g_id
                    },
                    pathimage_list = ps.pathimage_list
                });
            }
            return new JsonResult(new{data = "empty"});
        }
        [HttpPost]
        public IActionResult Post([FromBody] ProgramDto program)
        {
            return (_IProgram.AddProgram(program))?Ok(new Response { Status = "Success", Message = "Program created successfully!" }):BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] ProgramDto program)
        {
            if (_IProgram.CheckProgram(id))
            {
                return (_IProgram.UpdateProgram(id,program))?Ok(new Response { Status = "Success", Message = "Program updated successfully!" }):Ok(new Response { Status = "Success", Message = "Program updated fail!" });
            }
            else return BadRequest();         
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_IProgram.CheckProgram(id))
            {
                return (_IProgram.DeleteProgram(id))?Ok(new Response { Status = "Success", Message = "Program updated successfully!" }):Ok(new Response { Status = "Success", Message = "Program updated fail!" });
            }
            else return BadRequest();
        }

    }
}