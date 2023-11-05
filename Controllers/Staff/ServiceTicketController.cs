using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize]
    [Route("ServiceTicket")]
    [ApiController]
    public class ServiceTicketController : ControllerBase
    {

        private readonly ITicket _ticket;
        private readonly IBookTicket _bookTicket;
        private readonly IAccount _account;
        private readonly IEmailService _email;
        public ServiceTicketController(ITicket ticket, IBookTicket bookTicket, IAccount account, IEmailService email)
        {
            _ticket = ticket;
            _bookTicket = bookTicket;
            _account = account;
            _email = email;
        }
        private async Task<string> GetUserAsync() => await _account.GetUserIdAsync((string)Request.Headers["Authorization"]);
        [HttpGet]
        public async Task<ActionResult> GetEventTicket(int id) => Ok(await _ticket.GetListTicketAsync());
        //Order
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookTicketDto bt)
        {
            if(await _ticket.CheckFullTicketAsync(bt.TicketId)){
                if (await (_bookTicket.OrderTicketUserAsync(await GetUserAsync(), bt)))
                {
                    return Ok(new Response { Status = "Success", Message = "Book ticket successfully!" });
                }
            }
            return BadRequest(new Response { Status = "Fail", Message = "Ticket had sold out or not exist" });
        }
        [HttpPost("SendQREmail/{id}")]
        public async Task<ActionResult> Send(int id, [FromForm] string email)
        {
            
            //string x = _email.CreateQRCode("HelloWorld");
            await _email.SendAsync(await GetUserAsync(),id,email);
            return Ok("ok");
        }

    }
}