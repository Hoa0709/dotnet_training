using Microsoft.EntityFrameworkCore;
using app.Connects;
using app.Models;

namespace app.Repository
{
    public interface ILocations
    {
        public List<Location> GetListLocation();
        public Location GetLocationDetails(int id);
        public string GetLocationName(int id);
        public Boolean AddLocation(Location pr);
        public Boolean UpdateLocation(int id,Location l);
        public Boolean DeleteLocation(int id);
        public Boolean CheckLocation(int id);
    }
    public class LocationRepository : ILocations
    {
        private readonly AppDbContext _context;
        public LocationRepository( AppDbContext context)
        {
            _context = context;
        }

        public Boolean AddLocation(Location l)
        {
            try
            {
                _context.locations.Add(l);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckLocation(int id)
        {
            return _context.locations.Any(e => e.Id == id);
        }

        public bool DeleteLocation(int id)
        {
            try
            {
                Location n = _context.locations
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault();
                if (n != null)
                {
                    _context.locations.Remove(n);
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

        public List<Location> GetListLocation()
        {
            try
            {
                return _context.locations.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Location GetLocationDetails(int id)
        {
            try
            {
                Location n = _context.locations
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault();
                return n;
            }
            catch
            {
                throw;
            }
        }

        public string GetLocationName(int id)
        {
            try
            {
                Location n = _context.locations
                                    .Where(x => x.Id == id)
                                    .FirstOrDefault();
                if (n != null)
                {
                    return n.Title;
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

        public bool UpdateLocation(int id,Location l)
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
                return false;
            }
        }
    }
}