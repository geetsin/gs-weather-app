using Newtonsoft.Json;
using System.Net.Http.Headers;
using weather_app.Models.DataModels;
using weather_app.Models.ViewModels;
using weather_app.Utilities;

namespace weather_app.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _config;
        

        public WeatherService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<Tuple<string, string>> getCityLatitudeAndLongitude(string pCityName)
        {
            var openWeatherApiKey = _config["gs-weather-app-openweather-key"]; // API key for Prod
            if (Helper.isDevelopment)
            {
                openWeatherApiKey = _config["OpenWeather:ServiceApiKey"];
            }

            var apiParameters = $"?q={pCityName}&appid={openWeatherApiKey}";

            var client = new HttpClient();
            client.BaseAddress = new Uri(Helper.OPENWEATHERMAP_GEOCODING_API);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiParameters).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var openWeatherLatLongResponse = JsonConvert.DeserializeObject<List<OpenWeatherGeocodingObj>>(jsonString);

                string responseLatitude = openWeatherLatLongResponse[0].lat.ToString("F4");
                string responseLongitude = openWeatherLatLongResponse[0].lon.ToString("F4");
                //System.Diagnostics.Debug.WriteLine("LOG: Lat: " + responseLatitude + " | Lon: " + responseLongitude);
                return Tuple.Create(responseLatitude, responseLongitude);

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }

        }

        public async Task<OpenWeatherVM> getCityWeather(string pCityName)
        {
            var openWeatherApiKey = _config["gs-weather-app-openweather-key"]; // API key for Prod
            if (Helper.isDevelopment)
            {
                openWeatherApiKey = _config["OpenWeather:ServiceApiKey"];
            }

            Tuple<string,string> cityCoordinates = getCityLatitudeAndLongitude(pCityName).Result;

            var apiParameters = $"?lat={cityCoordinates.Item1}&lon={cityCoordinates.Item2}&appid={openWeatherApiKey}";


            var client = new HttpClient();
            client.BaseAddress = new Uri(Helper.OPENWEATHERMAP_WEATHER_API);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiParameters).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var openWeatherResponse = JsonConvert.DeserializeObject<OpenWeatherObj>(jsonString);

                OpenWeatherVM weatherData = new OpenWeatherVM()
                {
                    cityName = openWeatherResponse.name,
                    cityCountryCode = openWeatherResponse.sys.country,
                    cityTemp = Helper.getCelciusFromKelvin(openWeatherResponse.main.temp).ToString("0.0"), // Original Response is in Kelvin. Converting to Celcius
                    cityTempMax = Helper.getCelciusFromKelvin(openWeatherResponse.main.temp_max).ToString("0.0"),
                    cityTempMin = Helper.getCelciusFromKelvin(openWeatherResponse.main.temp_min).ToString("0.0"),
                    cityTempFeelsLike = Helper.getCelciusFromKelvin(openWeatherResponse.main.feels_like).ToString("0.0"),
                    cityHumidity = (openWeatherResponse.main.humidity).ToString("#.##"),
                    cityPressure = (openWeatherResponse.main.pressure).ToString("#.##"),
                    cityWeatherDescription = openWeatherResponse.weather[0].description,
                    cityWeatherIcon = openWeatherResponse.weather[0].icon
                };

                //System.Diagnostics.Debug.WriteLine("LOG: Lat: " + responseLatitude + " | Lon: " + responseLongitude);
                return weatherData;

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }


        // TODO: Implement this method with RestSharp
        public async Task<string> getLocationWallpaperUrl(string pLocation)
        {
            var unsplashApiKey = _config["gs-weather-app-unsplash-key"]; // API key for Prod
            if (Helper.isDevelopment)
            {
                unsplashApiKey = _config["Unsplash:ServiceApiKey"];
            }

            var apiParameters = $"?page=1&query={pLocation}&client_id={unsplashApiKey}&orientation=landscape";

            Random random = new Random();
            

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Helper.UNSPLASH_SEARCH_API);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiParameters).ConfigureAwait(false);


            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var unsplashResponse = JsonConvert.DeserializeObject<UnsplashResponse>(jsonString);
                var randomNo = random.Next(unsplashResponse.results.Count());
                Console.WriteLine(unsplashResponse.results[randomNo].urls.full);
                return unsplashResponse.results[randomNo].urls.regular;


            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }

        }
    }
}
