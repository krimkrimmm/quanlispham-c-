using TMDT.Models;

namespace TMDT.Services
{
    public interface ILocationService
    {
        List<City> GetCities();
        List<Districts> GetDistrictsByCity(int cityId);
    }
}
