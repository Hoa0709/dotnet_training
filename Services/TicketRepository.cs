using app.Connects;
using app.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public interface ITicket
    {
        public Task<List<Ticket>> GetListTicketAsync();
        public Task<TicketDetailDto> GetTicketDetailsAsync(int id);
        public Task<List<BookTicket>> GetListOderTicketAsync(int TicketId);
        public Task<List<BookTicket>> GetListTakeTicketAsync(int TicketId, bool TakeTicket);
        public Task<Boolean> AddTicketAsync(TicketDto t);
        public Task<Boolean> UpdateTicketAsync(int id,Ticket t);
        public Task<Boolean> DeleteTicketAsync(int id);
        public Task<Boolean> CheckTicketAsync(int id);
        public Task<Boolean> CheckFullTicketAsync(int id);
    }
    public class TicketRepository : ITicket
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TicketRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Ticket>> GetListTicketAsync()
        {
            try
            {
                return await _context.tickets.ToListAsync();
            }
            catch
            {
                throw new Exception("Error! When Get Tickets List");
            }
        }

        public async Task<TicketDetailDto> GetTicketDetailsAsync(int id)
        {
            try
            {
                Ticket t =  await _context.tickets
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();
                var p = await _context.programs
                        .Where(x => x.Id == t.ProgramId)
                        .Select(x => new {x.Name, x.Held_on })
                        .FirstOrDefaultAsync();
                TicketDetailDto td = _mapper.Map<TicketDetailDto>(t);
                td.Name = p.Name;
                td.Held_on = p.Held_on;
                return td;
                
            }
            catch
            {
                throw new Exception("Error! When Get Ticket");
            }
        }
        public async Task<List<BookTicket>> GetListOderTicketAsync(int TicketId)
        {
            try
            {
                return await _context.bookTickets
                        .Where(x => x.TicketId == TicketId)
                        .ToListAsync();
            }
            catch
            {
                throw new Exception("Error! When Get Your Ticket Order List");
            }
        }

        public async Task<bool> AddTicketAsync(TicketDto t)
        {
            try
            {
                HashCode hc = new HashCode();
                Ticket addt = _mapper.Map<Ticket>(t);
                addt.Code = hc.ComputeMd5Hash();
                addt.Inventory = 0;
                await _context.tickets.AddAsync(addt);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Error! When Add Ticket");
            }
        }

        public async Task<bool> UpdateTicketAsync(int id, Ticket t)
        {
            try
            {
                if(id == t.Id)
                {
                    _context.Entry(t).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                throw new Exception("Error! When Update Ticket");
            }
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            try
            {
                Ticket n = await _context.tickets
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
                if (n != null)
                {
                    _context.tickets.Remove(n);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
                }
            catch
            {
                throw new Exception("Error! When Delete Ticket");
            }
        }

        public async Task<bool> CheckTicketAsync(int id)
        {
            try
            {
                return await _context.tickets.AnyAsync(e => e.Id == id);
            }
            catch
            {
               throw new Exception("Error! When Check Ticket");
            }
            
        }

        public async Task<bool> CheckFullTicketAsync(int id)
        {
            try
            {
                return await _context.tickets.AnyAsync(e => e.Id == id && e.Quantity > e.Inventory);
            }
            catch
            {
               throw new Exception("Error! When Check Ticket");
            }
        }

        public async Task<List<BookTicket>> GetListTakeTicketAsync(int TicketId, bool TakeTicket)
        {
            try
            {
                return await _context.bookTickets
                        .Where(x => x.TicketId == TicketId && x.TakeTicket == TakeTicket)
                        .ToListAsync();
            }
            catch
            {
                
                throw new Exception("Error! When Get list take Ticket");
            }
        }
    }
}