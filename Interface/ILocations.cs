using app.Models;
namespace app.Interfaces
{
    public interface ILocations
    {
        public List<Location> GetListLocation();
        public Location GetLocationDetails(int id);
        public string GetLocationName(int id);
        public void AddLocation(Location pr);
        public void UpdateLocation(Location pr);
        public Location DeleteLocation(int id);
        public bool CheckLocation(int id);
    }
}