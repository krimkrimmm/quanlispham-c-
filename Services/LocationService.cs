using Microsoft.EntityFrameworkCore;
using TMDT.Data;
using TMDT.Models;

namespace TMDT.Services
{
    public class LocationService : ILocationService
    {
        private readonly TMDTDbContext _context;

        public LocationService(TMDTDbContext context)
        {
            _context = context;
        }

        public List<City> GetCities()
        {
            return _context.Cities.Include(c => c.Districts).ToList();
        }
        public List<Districts> GetDistrict()
        {
            return _context.Districts.ToList();
        }

        public List<Districts> GetDistrictsByCity(int cityId)
        {
            return _context.Districts.Where(d => d.CityId == cityId).ToList();
        }
    }
}
