using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize (Roles = "staff,manager")]
    [Route("CheckTicket")]
    [ApiController]
    public class CheckTickketController : ControllerBase
    {

        private readonly ITicket _ticket;
        private readonly IBookTicket _bookTicket;
        private readonly IAccount _account;
        public CheckTickketController(ITicket ticket, IBookTicket bookTicket, IAccount account)
        {
            _ticket = ticket;
            _bookTicket = bookTicket;
            _account = account;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Check(string id)
        {
            var x = await _bookTicket.CheckOrderAsync(id);
            if (x!=null)
                {
                    return Ok(x);
                }
            return BadRequest(new Response { Status = "Fail", Message = "Ticket not exist" });
        }

    }
}