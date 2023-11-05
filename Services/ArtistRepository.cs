using Microsoft.EntityFrameworkCore;
using app.Connects;
using app.Models;
using AutoMapper;

namespace app.Repository
{
    public interface IArtists
    {
        public Task<List<Artist>> GetListArtistAsync();
        public Task<Artist> GetArtistDetailsAsync(int id);
        public Task<Boolean> AddArtistAsync(ArtistDto a);
        public Task<Boolean> UpdateArtistAsync(int id,Artist a);
        public Task<Boolean> DeleteArtistAsync(int id);
        public Task<Boolean> CheckArtistAsync(int id);
    }
    public class ArtistRepository : IArtists
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ArtistRepository( AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Boolean> AddArtistAsync(ArtistDto a)
        {
            try
            {
                Artist adda = _mapper.Map<Artist>(a);
                adda.CreatAt = DateTime.Now;
                await _context.artists.AddAsync(adda);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Error! When Add Artist");
            }
        }

        public async Task<Boolean> CheckArtistAsync(int id)
        {
            return await _context.artists.AnyAsync(e => e.Id == id);
        }

        public async Task<Boolean> DeleteArtistAsync(int id)
        {
            try
            {
                Artist n = await _context.artists
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
                if (n != null)
                {
                    _context.artists.Remove(n);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
                }
            catch
            {
                throw new Exception("Error! When Delete Artist");
            }
        }

        public Task<List<Artist>> GetListArtistAsync()
        {
            try
            {
                return _context.artists.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Artist> GetArtistDetailsAsync(int id)
        {
            try
            {
                return await _context.artists
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
            }
            catch
            {
                throw new Exception("Error! When Get Artist");
            }
        }
        public async Task<Boolean> UpdateArtistAsync(int id,Artist l)
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
                throw new Exception("Error! When Update artist");
            }
        }
    }
}