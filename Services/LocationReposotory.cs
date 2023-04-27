using app.Connects;
using app.Interfaces;
using app.Models;

namespace app.Repository
{
    public class LocationRepository : ILocations
    {
        private readonly AppDbContext _context;
        public LocationRepository( AppDbContext context)
        {
            _context = context;
        }
        public void AddLocation(Location pr)
        {
            throw new NotImplementedException();
        }

        public bool CheckLocation(int id)
        {
            return _context.locations.Any(e => e.lid == id);
        }

        public Location DeleteLocation(int id)
        {
            throw new NotImplementedException();
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
                Location loca = _context.locations
                                    .Where(x => x.lid == id)
                                    .FirstOrDefault();
                if (loca != null)
                {
                    return loca;
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

        public string GetLocationName(int id)
        {
            try
            {
                Location loca = _context.locations
                                    .Where(x => x.lid == id)
                                    .FirstOrDefault();
                if (loca != null)
                {
                    return loca.title;
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

        public void UpdateLocation(Location pr)
        {
            throw new NotImplementedException();
        }
    }
}