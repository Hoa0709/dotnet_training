using Microsoft.EntityFrameworkCore;
using app.Connects;
using app.Models;
using AutoMapper;

namespace app.Repository
{
    public interface ILocations
    {
        public Task<List<Location>> GetListLocationAsync();
        public Task<Location> GetLocationDetailsAsync(int id);
        public Task<Boolean> AddLocationAsync(LocationDto l);
        public Task<Boolean> UpdateLocationAsync(int id,Location l);
        public Task<Boolean> DeleteLocationAsync(int id);
        public Task<Boolean> CheckLocationAsync(int id);
    }
    public class LocationRepository : ILocations
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public LocationRepository( AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Boolean> AddLocationAsync(LocationDto l)
        {
            try
            {
                Location addl = _mapper.Map<Location>(l);
                addl.CreatAt = DateTime.Now;
                await _context.locations.AddAsync(addl);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Error! When Add Location");
            }
        }

        public async Task<Boolean> CheckLocationAsync(int id)
        {
            return await _context.locations.AnyAsync(e => e.Id == id);
        }

        public async Task<Boolean> DeleteLocationAsync(int id)
        {
            try
            {
                Location n = await _context.locations
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
                if (n != null)
                {
                    _context.locations.Remove(n);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
                }
            catch
            {
                throw new Exception("Error! When Delete Location");
            }
        }

        public async Task<List<Location>> GetListLocationAsync()
        {
            try
            {
                return await _context.locations.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Location> GetLocationDetailsAsync(int id)
        {
            try
            {
                return await _context.locations
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
            }
            catch
            {
                throw new Exception("Error! When Get Location");
            }
        }

        public async Task<Boolean> UpdateLocationAsync(int id,Location l)
        {
            try
            {
                if(id == l.Id)
                {
                    _context.Entry(l).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                throw new Exception("Error! When Update location");
            }
        }
    }
}