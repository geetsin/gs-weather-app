namespace weather_app.Models.DataModels
{
    public class CityObj
    {
        public Fields fields { get; set; }
        public Geometry geometry { get; set; }
        public class Fields
        {
            public string alternate_names { get; set; }
            public string ascii_name { get; set; }
            public List<double> coordinates { get; set; }
            public string cou_name_en { get; set; }
            public string country_code { get; set; }
            public string elevation { get; set; }
            public string label_en { get; set; }
            public string name { get; set; }
            public int population { get; set; }
            public string timezone { get; set; }
        }

        public class Geometry
        {
            public List<double> coordinates { get; set; }
            public string type { get; set; }
        }
    }
}
