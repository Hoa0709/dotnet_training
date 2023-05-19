using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IPrograms _program;
        private readonly ILocations _location;
        private readonly INews _news;
        private readonly IArtists _artist;
        public HomeController(IPrograms program,ILocations location,INews news, IArtists artist)
        {
            _program = program;
            _location = location;
            _news = news;
            _artist = artist;
        }
        [HttpGet]
        
        public ActionResult Index()
        {
            return Ok();
        }
        [HttpGet("Events")]
        public async Task<ActionResult> Events()
        {
            return Ok(await _program.GetListProgramsAsync());
        }
        //Get Item Program
        [HttpGet("Events/{id}")]
        public async Task<ActionResult> Eventsid(int id)
        {
            if (await _program.CheckProgramAsync(id))
            {
                return Ok(await _program.GetProgramDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Event" });
        }
        [HttpGet("Location")]
        public async Task<ActionResult> Location()
        {
            return Ok(await _location.GetListLocationAsync());
        }
        //Get Item Program
        [HttpGet("Location/{id}")]
        public async Task<ActionResult> Locationid(int id)
        {
            if (await _location.CheckLocationAsync(id))
            {
                return Ok(await _location.GetLocationDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Location" });
        }
        [HttpGet("News")]
        public async Task<ActionResult> News()
        {
            return Ok(await _news.GetListNewsAsync());
        }
        //Get Item Program
        [HttpGet("News/{id}")]
        public async Task<ActionResult> Newsid(int id)
        {
            if (await _news.CheckNewsAsync(id))
            {
                return Ok(await _news.GetNewsDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found News" });
        }
        [HttpGet("Artists")]
        public async Task<ActionResult> Artists()
        {
            return Ok(await _artist.GetListArtistAsync());
        }
        //Get Item Program
        [HttpGet("Artists/{id}")]
        public async Task<ActionResult> Artistsid(int id)
        {
            if (await _artist.CheckArtistAsync(id))
            {
                return Ok(await _artist.GetArtistDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Artist" });
        }
    }
}