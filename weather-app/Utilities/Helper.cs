namespace weather_app.Utilities
{
    public static class Helper
    {
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
