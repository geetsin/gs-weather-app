namespace weather_app.Services
{
    public interface ICityService
    {
        public Task<List<string>> GetCityAutocompleteSuggestions(string pInput);
    }
}
