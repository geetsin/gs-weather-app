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
            IEnumerable<CityObj> cityDataList = JsonSerializer.Deserialize<IEnumerable<CityObj>>(cityJsonData)!;


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

        public async Task<List<CityDetailsObj>> GetCityDetails(string pInput)
        {
            List<CityDetailsObj> cityDetailsList = new List<CityDetailsObj>();

            var rootPath = _hostEnvironment.ContentRootPath;
            var fullPath = Path.Combine(rootPath, Helper.CITY_DATA_LOCATION);

            var cityJsonData = await File.ReadAllTextAsync(fullPath);
            IEnumerable<CityObj> cityDataList = JsonSerializer.Deserialize<IEnumerable<CityObj>>(cityJsonData)!;

            foreach (var cityObj in cityDataList)
            {
                if (cityObj.fields.name.ToUpper() == pInput.ToUpper())
                {
                    CityDetailsObj cityDetailsObj = new CityDetailsObj();
                    cityDetailsObj.city_name = cityObj.fields.name;
                    cityDetailsObj.alternate_names = cityObj.fields.alternate_names;
                    cityDetailsObj.ascii_name = cityObj.fields.ascii_name;
                    cityDetailsObj.country_name = cityObj.fields.cou_name_en;
                    cityDetailsObj.country_code = cityObj.fields.country_code;
                    cityDetailsObj.elevation = cityObj.fields.elevation;
                    cityDetailsObj.population = cityObj.fields.population;
                    cityDetailsObj.timezone = cityObj.fields.timezone;

                    cityDetailsList.Add(cityDetailsObj);
                }
            }

            return cityDetailsList;
        }
    }
}
