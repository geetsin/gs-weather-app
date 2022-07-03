namespace weather_app.Utilities
{
    public static class Helper
    {
        public static int CODE_FAILURE = 0;
        public static int CODE_SUCCESS = 1;

        public static string CITY_DATA_LOCATION = "Data/city_data_simplified.json";

        // A global variable to check if the env is Development
        public static bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        // API LISTS
        public static string UNSPLASH_SEARCH_API = "https://api.unsplash.com/search/photos";
        public static string OPENWEATHERMAP_GEOCODING_API = "http://api.openweathermap.org/geo/1.0/direct";
        public static string OPENWEATHERMAP_WEATHER_API = "https://api.openweathermap.org/data/2.5/weather";


        // Helper Methods
        public static double getCelciusFromKelvin(double kelvinValue)
        {
            return (kelvinValue - 273.15);
        }
    }
}
