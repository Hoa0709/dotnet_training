using Microsoft.AspNetCore.Mvc;
using app.Models;
using app.Repository;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    [Authorize(Roles = "manager")]
    [Route("Tickets-Manager")]
    [Route("Manager/Tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        
        private readonly ITicket _ticket;
        private readonly IPrograms _program;
        public TicketController(ITicket ticket,IPrograms program)
        {
            _ticket = ticket;
            _program = program;
        }
        //Get List Ticket
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _ticket.GetListTicketAsync());
        }
        //Get Item Program
        [HttpGet("GetDetail/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (await _ticket.CheckTicketAsync(id))
            {
                return Ok(await _ticket.GetTicketDetailsAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Ticket" });
        }
        [HttpGet("GetOrder/{id}")]
        public async Task<ActionResult> Order(int id)
        {
            if (await _ticket.CheckTicketAsync(id))
            {
                return Ok(await _ticket.GetListOderTicketAsync(id));
            }
            return NotFound(new Response { Status = "Fail", Message = "Not Found Order Ticket" });
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketDto Ticket)
        {
            if (await _program.CheckTypeProgramAsync(Ticket.ProgramId))
            {
                if (await _ticket.AddTicketAsync(Ticket)){
                    return Ok(new Response { Status = "Success", Message = "Ticket created successfully!" });
                }
            } else return BadRequest(new Response { Status = "Fail", Message = "Program not ticket or not exist" });
            return BadRequest(new Response { Status = "Fail", Message = "Ticket created fail cuz program have create ticket!" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] Ticket Ticket)
        {
            if (await _ticket.CheckTicketAsync(id))
            {
                if (await _ticket.UpdateTicketAsync(id,Ticket)){
                    return Ok(new Response { Status = "Success", Message = "Ticket updated successfully!" });
                }
            }
            return BadRequest(new Response { Status = "Fail", Message = "Ticket updated fail!" });
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _ticket.CheckTicketAsync(id))
            {
                if (await _ticket.DeleteTicketAsync(id)){
                    return Ok(new Response { Status = "Success", Message = "Ticket deleted!" });
                }
            }
            return BadRequest(new Response { Status = "Fail", Message = "Ticket deleted fail!" });
        }

    }
}