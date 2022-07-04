using weather_app.Models.DataModels;

namespace weather_app.Services
{
    public interface ICityService
    {
        public Task<List<string>> GetCityAutocompleteSuggestions(string pInput);
        public Task<List<CityDetailsObj>> GetCityDetails(string pInput);
    }
}
