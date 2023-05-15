using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize(Roles = "manager")]
    [Route("Locations-Manager")]
    [Route("Manager/Locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        
        private readonly ILocations _location;
        public LocationController(ILocations location)
        {
            _location = location;
        }
        //Get List News
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _location.GetListLocationAsync());
        }
        //Get Item Program
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await _location.CheckLocationAsync(id))
            {
                return Ok(await _location.GetLocationDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Location" });
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LocationDto location)
        {
            return (await _location.AddLocationAsync(location))?Ok(new Response { Status = "Success", Message = "Location created successfully!" }):BadRequest(new Response { Status = "Fail", Message = "Location created fail!" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] Location location)
        {
            if (await _location.CheckLocationAsync(id))
            {
                if(await _location.UpdateLocationAsync(id,location)){
                    return Ok(new Response { Status = "Success", Message = "Location updated successfully!" });
                }
            }
            return BadRequest(new Response { Status = "Fail", Message = "Location updated fail!" });   
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _location.CheckLocationAsync(id))
            {
                if (await _location.DeleteLocationAsync(id)){
                    return Ok(new Response { Status = "Success", Message = "Location deleted successfully!" });
                }
            }
            return BadRequest(new Response { Status = "Fail", Message = "Location deleted fail!" });
        }

    }
}