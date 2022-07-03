using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using weather_app.Models.DataModels;
using weather_app.Utilities;

namespace weather_app.Services
{
    public class CityService : ICityService
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMemoryCache _memoryCache;

        public CityService(IWebHostEnvironment hostEnvironment, IMemoryCache memoryCache)
        {
            _hostEnvironment = hostEnvironment;
            _memoryCache = memoryCache;
        }
        public async Task<List<string>> GetCityAutocompleteSuggestions(string pInput)
        {
            List<String> cityAutocompleteSuggestionsList = new List<string>();

            var rootPath = _hostEnvironment.ContentRootPath;
            var fullPath = Path.Combine(rootPath, Helper.CITY_DATA_LOCATION);

            var cityJsonData = await File.ReadAllTextAsync(fullPath);
            IEnumerable<CityAutocompleteObj> cityDataList = JsonSerializer.Deserialize<IEnumerable<CityAutocompleteObj>>(cityJsonData)!;


            foreach (var cityAutocompleteObj in cityDataList)
            {
                if (cityAutocompleteObj.fields.name.ToUpper().StartsWith(pInput.ToUpper()))
                {
                    string cityName = $"{cityAutocompleteObj.fields.name}, {cityAutocompleteObj.fields.country_code}";
                    cityAutocompleteSuggestionsList.Add(cityName);
                }
            }

            return cityAutocompleteSuggestionsList;
        }
    }
}
