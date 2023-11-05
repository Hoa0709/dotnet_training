using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize(Roles = "manager")]
    [Route("Artists-Manager")]
    [Route("Manager/Artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        
        private readonly IArtists _artist;
        public ArtistController(IArtists artist)
        {
            _artist = artist;
        }
        //Get List News
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _artist.GetListArtistAsync());
        }
        //Get Item Program
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await _artist.CheckArtistAsync(id))
            {
                return Ok(await _artist.GetArtistDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Artist" });
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ArtistDto artist)
        {
            return (await _artist.AddArtistAsync(artist))?Ok(new Response { Status = "Success", Message = "Artist created successfully!" }):BadRequest(new Response { Status = "Fail", Message = "Artist created fail!" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] Artist artist)
        {
            if (await _artist.CheckArtistAsync(id))
            {
                if (await _artist.UpdateArtistAsync(id,artist)){
                    return Ok(new Response { Status = "Success", Message = "Artist updated successfully!" });
                }
            }
            return BadRequest(new Response { Status = "Fail", Message = "Artist updated fail!" });
            
        }

        [HttpDelete("{id}")]
        public  async Task<ActionResult> Delete(int id)
        {
            if (await _artist.CheckArtistAsync(id))
            {
                if(await _artist.DeleteArtistAsync(id)){
                    return Ok(new Response { Status = "Success", Message = "Artist deleted successfully!" });
                }
            }
            return BadRequest(new Response { Status = "Fail", Message = "Artist deleted fail!" });
        }

    }
}