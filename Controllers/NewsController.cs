using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;

namespace app.Controllers
{
    [Route("News")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        
        private readonly INews _Inews;
        public NewsController(INews Inews)
        {
            Inews = _Inews;
        }
        //Get List News
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return new JsonResult(await Task.FromResult(_Inews.GetListNews()));
        }
        //Get Item Program
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await Task.FromResult(_Inews.CheckNews(id)))
            {
                var ps = await Task.FromResult(_Inews.GetNewsDetails(id));
                if (ps != null)
                {
                    return Ok(ps);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Post([FromBody] News news)
        {
            return (_Inews.Addnews(news))?Ok(new Response { Status = "Success", Message = "News created successfully!" }):BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] News news)
        {
            if (_Inews.CheckNews(id))
            {
                return (_Inews.UpdateNews(id,news))?Ok(new Response { Status = "Success", Message = "News updated successfully!" }):Ok(new Response { Status = "Success", Message = "Program updated fail!" });
            }
            else return BadRequest();
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_Inews.CheckNews(id))
            {
                return (_Inews.DeleteNews(id))?Ok(new Response { Status = "Success", Message = "News deleted successfully!" }):Ok(new Response { Status = "Success", Message = "Program updated fail!" });
            }
            else return BadRequest();
        }

    }
}