using app.Connects;
using app.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace app.Repository
{
    public interface INews
    {
        public Task<List<News>> GetListNewsAsync();
        public Task<News> GetNewsDetailsAsync(int id);
        public Task<Boolean> AddNewsAsync(NewsDto n);
        public Task<Boolean> UpdateNewsAsync(int id,News n);
        public Task<Boolean> DeleteNewsAsync(int id);
        public Task<Boolean> CheckNewsAsync(int id);
    }
    public class NewsRepository : INews
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public NewsRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<News>> GetListNewsAsync()
        {
            try
            {
                return await _context.news.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<News> GetNewsDetailsAsync(int id)
        {
            try
            {
                return await _context.news
                            .Where(x => x.nid == id)
                            .FirstOrDefaultAsync();
            }
            catch
            {
                throw new Exception("Error! When Get News");
            }
        }

        public async Task<Boolean> AddNewsAsync(NewsDto n)
        {
            try
            {
                News addn = _mapper.Map<News>(n);
                addn.postdate = DateTime.Now;
                await _context.news.AddAsync(addn);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Error! When Add News");
            }
        }

        public async Task<Boolean> UpdateNewsAsync(int id,News n)
        {
            try
            {
                if(id == n.nid)
                {
                    _context.Entry(n).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                throw new Exception("Error! When Update News");
            }
        }

        public async Task<Boolean> DeleteNewsAsync(int id)
        {
            try
            {
                News n = await _context.news
                                    .Where(x => x.nid == id)
                                    .FirstOrDefaultAsync();
                if (n != null)
                {
                    _context.news.Remove(n);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
                }
            catch
            {
                throw new Exception("Error! When Delete News");
            }
        }

        public async Task<Boolean> CheckNewsAsync(int id)
        {
            return await _context.news.AnyAsync(e => e.nid == id);
        }
    }
}