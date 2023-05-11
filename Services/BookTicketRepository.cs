using app.Connects;
using app.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;

namespace app.Repository
{
    public interface IBookTicket
    {
        public Task<List<BookTicket>> GetListBookTicketUserAsync(string UserId);
        public Task<BookTicket> GetDetailBookTicketUserAsync(int BookTicketID);
        public Task<BookTicket> CheckOrderAsync(string code);
        public Task<Boolean> OrderTicketUserAsync(string UserId,BookTicketDto bt);
        public Task<Boolean> UpdateBookTicketAsync(string UserId,int BookTicketID,BookTicket bt);
        public Task<Boolean> DeleteBookTicketAsync(string UserId,int BookTicketID);
        public Task<Boolean> CheckBookTicketAsync(string UserId,int BookTicketID);
        public Task<String> GetQRCodeAsync(string UserId,int BookTicketID);
    }
    public class BookTicketRepository : IBookTicket
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public BookTicketRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context,IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _mapper = mapper;
            _context = context;
        } 

        public async Task<List<BookTicket>> GetListBookTicketUserAsync(string UserId)
        {
            try
            {
                return await _context.bookTickets
                        .Where(x => x.UserId == UserId)
                        .ToListAsync();
            }
            catch
            {
                throw new Exception("Error! When Get Your Ticket Order List");
            }
        }

        public async Task<Boolean> OrderTicketUserAsync(string UserId,BookTicketDto bt)
        {
            try
            {
                Ticket t = await _context.tickets
                                    .Where(x => x.Id == bt.TicketId)
                                    .FirstOrDefaultAsync();
                if (bt.NumberOfTickets > (t.Quantity - t.Inventory))
                {
                    throw new Exception("Number of ticket not enough to book");
                }
                t.Inventory = t.Inventory + bt.NumberOfTickets;
                _context.Entry(t).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                HashCode hc = new HashCode();
                BookTicket addbt = _mapper.Map<BookTicket>(bt);
                addbt.Code = hc.ComputeSha256Hash();
                addbt.UserId = UserId;
                addbt.CreateAt = DateTime.Now;
                await _context.bookTickets.AddAsync(addbt);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Error when book ticket! Let try!!!");
            }
        }

        public async Task<Boolean> UpdateBookTicketAsync(string UserId,int BookTicketID,BookTicket bt)
        {
            try
            {
                if (!await CheckBookTicketAsync(UserId,BookTicketID) && (bt.Id == BookTicketID))
                {
                    _context.Entry(bt).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                else throw new Exception("Not Found Book Ticket! Please Try Again");
            }
            catch
            {
                throw new Exception("Error when update book ticket! Let try!!!");
            }
        }

        public async Task<Boolean> DeleteBookTicketAsync(string UserId,int BookTicketID)
        {
            try
            {
                if (!await CheckBookTicketAsync(UserId,BookTicketID)){
                    throw new Exception("Not Found Book Ticket in User! Please Try Again");
                }
                BookTicket n = _context.bookTickets
                                    .Where(x => x.Id == BookTicketID)
                                    .FirstOrDefault();
                if (n != null)
                {
                    _context.bookTickets.Remove(n);
                    _context.SaveChanges();
                    return true;
                }
                else
                    throw new Exception("Book Ticket is Null! Please Check and Try Again");
            }
            catch
            {
                throw new Exception("Error when delete book ticket! Let try!!!");
            }
        }

        public async Task<Boolean> CheckBookTicketAsync(string UserId,int BookTicketID){
            return await _context.bookTickets.AnyAsync(e => e.Id == Convert.ToInt32(BookTicketID) && e.UserId.Equals(UserId));
        }

        public async Task<BookTicket> GetDetailBookTicketUserAsync(int BookTicketID)
        {
            try
            {
                return await _context.bookTickets
                                .Where(x => x.Id == BookTicketID)
                                .FirstOrDefaultAsync();
            }
            catch
            {
                throw new Exception("Error when find book ticket! Let try!!!");
            }
        }

        public async Task<string> GetQRCodeAsync(string UserId, int BookTicketID)
        {
            try
            {
                return Convert.ToString( await _context.bookTickets
                                .Where(x => x.Id == BookTicketID)
                                .Select(x => x.Code)
                                .FirstOrDefaultAsync());
            }
            catch
            {
                throw new Exception("Error when find book ticket! Let try!!!");
            }
        }

        public async Task<BookTicket> CheckOrderAsync(string code)
        {
            try
            {
                return await Task.FromResult(_context.bookTickets
                                .Where(x => x.Code == code)
                                .FirstOrDefault());
            }
            catch
            {
                
                throw new Exception("Error when check!!!");
            }
        }
    }
}