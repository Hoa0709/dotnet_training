using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize(Roles = "manager")]
    [Route("News-Manager")]
    [Route("Manager/News")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        
        private readonly INews _news;
        public NewsController(INews news)
        {
            _news = news;
        }
        //Get List News
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _news.GetListNewsAsync());
        }
        //Get Item Program
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await _news.CheckNewsAsync(id))
            {
                return Ok(await _news.GetNewsDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found News" });
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewsDto news)
        {
            return (await _news.AddNewsAsync(news))?Ok(new Response { Status = "Success", Message = "News created successfully!" }):BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] News news)
        {
            if (await _news.CheckNewsAsync(id))
            {
                if (await _news.UpdateNewsAsync(id,news)){
                    return Ok(new Response { Status = "Success", Message = "News updated successfully!" });
                }
            }
            return BadRequest(new Response { Status = "Success", Message = "Program updated fail!" });
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _news.CheckNewsAsync(id))
            {
                if (await _news.DeleteNewsAsync(id)) return Ok(new Response { Status = "Success", Message = "News deleted successfully!" });
            }
            return BadRequest(new Response { Status = "Success", Message = "Program updated fail!" });
        }

    }
}