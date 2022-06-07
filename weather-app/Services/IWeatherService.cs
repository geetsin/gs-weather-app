using weather_app.Models.DataModels;
using weather_app.Models.ViewModels;

namespace weather_app.Services
{
    public interface IWeatherService
    {
        public Task<string> getLocationWallpaperUrl(string pLocation);
        public Task<Tuple<string, string>> getCityLatitudeAndLongitude(string pCityName);
        public Task<OpenWeatherVM> getCityWeather(string pCityName);
    }
}
