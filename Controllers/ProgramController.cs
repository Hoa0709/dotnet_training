using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;
using app.Interfaces;
using System.Collections;

namespace app.Controllers
{
    [Route("Program")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        
        private readonly IPrograms _IProgram;
        private readonly ILocations _ILocation;
        public ProgramController(IPrograms IProgram,ILocations ILocation)
        {
            _IProgram = IProgram;
            _ILocation = ILocation;
        }
        //Get List Program
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var ps = await Task.FromResult(_IProgram.GetListPrograms());
            var m = new ArrayList();
            foreach (var item in ps)
            {
                m.Add(new{
                    id_chuongtrinh = item.pid,
                    chuongtrinh_name = item.name,
                    pathimage_list = item.pathimage_list
                });
            }
            return new JsonResult(m);
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
                    price = 20,
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
        public IActionResult Post([FromForm] ProgramInfo program)
        {
            _IProgram.AddProgram(program);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromForm] ProgramInfo program)
        {
            if (_IProgram.CheckProgram(id))
            {
                program.pid = id;
                _IProgram.UpdateProgram(program);
                return Ok();
            }
            else return BadRequest();
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new JsonResult(new{result = (_IProgram.DeleteProgram(id))?"ok":"error"});
        }

    }
}