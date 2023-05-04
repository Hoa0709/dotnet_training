using app.Connects;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public interface INews
    {
        public List<News> GetListNews();
        public News GetNewsDetails(int id);
        public Boolean Addnews(News n);
        public Boolean UpdateNews(int id,News n);
        public Boolean DeleteNews(int id);
        public Boolean CheckNews(int id);
    }
    public class NewsRepository : INews
    {
        private readonly AppDbContext _context;
        public NewsRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<News> GetListNews()
        {
            try
            {
                return _context.news.ToList();
            }
            catch
            {
                throw;
            }
        }

        public News GetNewsDetails(int id)
        {
            try
            {
                News n = _context.news
                                    .Where(x => x.nid == id)
                                    .FirstOrDefault();
                return n;
            }
            catch
            {
                throw;
            }
        }

        public Boolean Addnews(News n)
        {
            try
            {
                _context.news.Add(n);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean UpdateNews(int id,News n)
        {
            try
            {
                if(id != n.nid) return false;
                else{
                    _context.Entry(n).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean DeleteNews(int id)
        {
            try
            {
                News n = _context.news
                                    .Where(x => x.nid == id)
                                    .FirstOrDefault();
                if (n != null)
                {
                    _context.news.Remove(n);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
                }
            catch
            {
                throw;
            }
        }

        public Boolean CheckNews(int id)
        {
            return _context.news.Any(e => e.nid == id);
        }
    }
}