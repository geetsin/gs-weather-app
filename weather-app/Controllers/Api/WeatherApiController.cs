using Microsoft.AspNetCore.Mvc;
using weather_app.Models.ViewModels;
using weather_app.Services;

namespace weather_app.Controllers.Api
{
    [Route("api")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class WeatherApiController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherApiController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        [Route("wallpaper/{pLocationName}")]
        public IActionResult GetLocationWallpaper(string pLocationName)
        {
            CommonResponse<string> commonResponse = new CommonResponse<string>();
            try
            {
                string locationWallpaperUrl = _weatherService.getLocationWallpaperUrl(pLocationName).Result;
                if (locationWallpaperUrl != null)
                {
                    commonResponse.dataenum = locationWallpaperUrl;
                    commonResponse.message = "Location Wallpaper received";
                    commonResponse.status = 1;
                }
                else
                {
                    commonResponse.message = "Error in receving location wallpaper";
                    commonResponse.status = 2;
                }
            } catch (Exception ex)
            {
                commonResponse.message = ex.Message;
                commonResponse.status = 0;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("weather/{pLocationName}")]
        public IActionResult GetLocationWeather(string pLocationName)
        {
            CommonResponse<OpenWeatherVM> commonResponse = new CommonResponse<OpenWeatherVM>();
            try
            {
                OpenWeatherVM locationWeather = _weatherService.getCityWeather(pLocationName).Result;
                if(locationWeather != null)
                {
                    commonResponse.dataenum = locationWeather;
                    commonResponse.message = "Locationweather received";
                    commonResponse.status = 1;
                }
                else
                {
                    commonResponse.message = "Error finding location weather";
                    commonResponse.status= 2;
                }
            } catch(Exception ex)
            {
                commonResponse.message=ex.Message;
                commonResponse.status = 0;
            }
            return Ok(commonResponse);
            

        }
    }
}
