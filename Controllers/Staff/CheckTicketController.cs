using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize (Roles = "staff,manager")]
    [Route("CheckTicket")]
    [ApiController]
    public class CheckTicketController : ControllerBase
    {

        private readonly ITicket _ticket;
        private readonly IBookTicket _bookTicket;
        private readonly IAccount _account;
        public CheckTicketController(ITicket ticket, IBookTicket bookTicket, IAccount account)
        {
            _ticket = ticket;
            _bookTicket = bookTicket;
            _account = account;
        }

        public class Code
        {
            public string code { get; set;}
        }
        [HttpPost("CheckQRCode")]
        public async Task<IActionResult> Check([FromBody]Code code)
        {
            var x = await _bookTicket.CheckQRCodeAsync(code.code);
            if (x!=null)
                {
                    return Ok(x);
                }
            return BadRequest(new Response { Status = "Fail", Message = "Ticket not exist" });
        }
        [HttpPut("ConfirmQRCode/{code}")]
        public async Task<IActionResult> Confirm(string code)
        {
            if (await _bookTicket.ConfirmQRCodeAsync(code))
                {
                    return Ok(new Response { Status = "Success", Message = "Confirm ticket ok" });
                }
            return BadRequest(new Response { Status = "Fail", Message = "Error! Let try !!!" });
        }

    }
}