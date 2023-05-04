using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;

namespace app.Controllers
{
    [Route("Artist")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        
        private readonly IArtists _IArtist;
        public ArtistController(IArtists IArtist)
        {
            _IArtist = IArtist;
        }
        //Get List News
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return new JsonResult(await Task.FromResult(_IArtist.GetListArtist()));
        }
        //Get Item Program
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await Task.FromResult(_IArtist.CheckArtist(id)))
            {
                var ps = await Task.FromResult(_IArtist.GetArtistDetails(id));
                if (ps != null)
                {
                    return Ok(ps);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Post([FromBody] Artist artist)
        {
            return (_IArtist.AddArtist(artist))?Ok(new Response { Status = "Success", Message = "Artist created successfully!" }):BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Artist artist)
        {
            if (_IArtist.CheckArtist(id))
            {
                return (_IArtist.UpdateArtist(id,artist))?Ok(new Response { Status = "Success", Message = "Artist updated successfully!" }):BadRequest();
            }
            else return BadRequest();
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_IArtist.CheckArtist(id))
            {
                return (_IArtist.DeleteArtist(id))?Ok(new Response { Status = "Success", Message = "Location deleted successfully!" }):BadRequest();
            }
            else return BadRequest();
        }

    }
}