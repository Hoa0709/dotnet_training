using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;

namespace app.Controllers
{
    [Route("Location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        
        private readonly ILocations _ILocation;
        public LocationController(ILocations ILocation)
        {
            ILocation = _ILocation;
        }
        //Get List News
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return new JsonResult(await Task.FromResult(_ILocation.GetListLocation()));
        }
        //Get Item Program
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await Task.FromResult(_ILocation.CheckLocation(id)))
            {
                var ps = await Task.FromResult(_ILocation.GetLocationDetails(id));
                if (ps != null)
                {
                    return Ok(ps);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Location location)
        {
            return (_ILocation.AddLocation(location))?Ok(new Response { Status = "Success", Message = "Location created successfully!" }):BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Location location)
        {
            if (_ILocation.CheckLocation(id))
            {
                return (_ILocation.UpdateLocation(id,location))?Ok(new Response { Status = "Success", Message = "Location updated successfully!" }):BadRequest();
            }
            else return BadRequest();
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_ILocation.CheckLocation(id))
            {
                return (_ILocation.DeleteLocation(id))?Ok(new Response { Status = "Success", Message = "Location deleted successfully!" }):BadRequest();
            }
            else return BadRequest();
        }

    }
}