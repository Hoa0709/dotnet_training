using Microsoft.EntityFrameworkCore;
using app.Connects;
using app.Models;

namespace app.Repository
{
    public interface IArtists
    {
        public List<Artist> GetListArtist();
        public Artist GetArtistDetails(int id);
        public string GetArtistName(int id);
        public Boolean AddArtist(Artist pr);
        public Boolean UpdateArtist(int id,Artist pr);
        public Boolean DeleteArtist(int id);
        public Boolean CheckArtist(int id);
    }
    public class ArtistRepository : IArtists
    {
        private readonly AppDbContext _context;
        public ArtistRepository( AppDbContext context)
        {
            _context = context;
        }

        public Boolean AddArtist(Artist l)
        {
            try
            {
                _context.artists.Add(l);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckArtist(int id)
        {
            return _context.artists.Any(e => e.Id == id);
        }

        public bool DeleteArtist(int id)
        {
            try
            {
                Artist n = _context.artists
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault();
                if (n != null)
                {
                    _context.artists.Remove(n);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
                }
            catch
            {
                return false;
            }
        }

        public List<Artist> GetListArtist()
        {
            try
            {
                return _context.artists.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Artist GetArtistDetails(int id)
        {
            try
            {
                Artist n = _context.artists
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault();
                if (n != null)
                {
                    return n;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public string GetArtistName(int id)
        {
            try
            {
                Artist n = _context.artists
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault();
                if (n != null)
                {
                    return n.Name;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateArtist(int id,Artist l)
        {
            try
            {
                if(id != l.Id) return false;
                else{
                    _context.Entry(l).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}