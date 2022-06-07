using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using weather_app.Controllers.Api;
using weather_app.Services;

namespace weather_app_test
{
    public class UnitTestWeatherApp
    {
        IConfiguration _config;
        IWeatherService _weatherService;
        WeatherApiController apiController;


        [SetUp]
        public void Setup()
        {
            _weatherService = new WeatherService(_config);
            apiController = new WeatherApiController(_weatherService);

        }

        [Test]
        public void GetLocationWallpaperUrl_Works()
        {
            // Arrange
            var testCityName = "canberra";

            //Act
            var result = apiController.GetLocationWallpaper(testCityName);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public void GetLocationWeather_Works()
        {
            // Arrange
            var testCityName = "canberra";

            // Act
            var okResult = apiController.GetLocationWeather(testCityName) as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}