using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize]
    [Route("MyTicketOrder")]
    [ApiController]
    public class BookTicketController : ControllerBase
    {

        private readonly ITicket _ticket;
        private readonly IBookTicket _bookTicket;
        private readonly IAccount _account;
        public BookTicketController(ITicket ticket, IBookTicket bookTicket, IAccount account)
        {
            _ticket = ticket;
            _bookTicket = bookTicket;
            _account = account;
        }
        private async Task<string> GetUserAsync() => await _account.GetUserIdAsync((string)Request.Headers["Authorization"]);

        //Get List
        [HttpGet]
        public async Task<IActionResult> Get(){
            string userId = await GetUserAsync();
            if (! await _account.CheckAccountAsync(userId))
            {
                return BadRequest(new Response { Status = "Fail", Message = "Error! Please Login again!!!" });   
            }
            return Ok(await _bookTicket.GetListBookTicketUserAsync(userId));
        }
        //Get Item
        [HttpGet("GetOrder/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            string userId = await GetUserAsync();
            if (! await _account.CheckAccountAsync(userId))
            {
                return BadRequest(new Response { Status = "Fail", Message = "Error! Please Login again!!!" });   
            }
            if (await _bookTicket.CheckBookTicketAsync(userId, id))
            {
                var order = await _bookTicket.GetDetailBookTicketUserAsync(id);
                return Ok(order);
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Your Ticket Order" });
        }

        //GetQR
        [HttpGet("GetQR/{id}")]
        public async Task<ActionResult> GetQR(int id)
        {
            string userId = await GetUserAsync();
            if (! await _account.CheckAccountAsync(userId))
            {
                return BadRequest(new Response { Status = "Fail", Message = "Error! Please Login again!!!" });   
            }
            if (await _bookTicket.CheckBookTicketAsync(userId, id))
            {
                string code = await _bookTicket.GetQRCodeAsync(userId, id);
                //var location = new Uri($"{Request.Scheme}://{Request.Host}");
                //string url = Convert.ToString(location.AbsoluteUri);
                return Ok(new
                {
                    Status = "Success",
                    QRCode = code
                });
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Your Ticket Order" });
        }
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

    }
}