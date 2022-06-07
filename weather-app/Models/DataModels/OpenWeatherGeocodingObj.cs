namespace weather_app.Models.DataModels
{

    public class OpenWeatherGeocodingObj
    {

        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }

        public class LocalNames
        {
            public string ja { get; set; }
            public string eo { get; set; }
            public string es { get; set; }
            public string el { get; set; }
            public string en { get; set; }
            public string bn { get; set; }
            public string oc { get; set; }
            public string te { get; set; }
            public string uk { get; set; }
            public string hi { get; set; }
            public string ko { get; set; }
            public string my { get; set; }
            public string ar { get; set; }
            public string he { get; set; }
            public string cs { get; set; }
            public string pt { get; set; }
            public string ne { get; set; }
            public string ms { get; set; }
            public string fa { get; set; }
            public string pa { get; set; }
            public string ku { get; set; }
            public string ur { get; set; }
            public string th { get; set; }
            public string ml { get; set; }
            public string de { get; set; }
            public string ta { get; set; }
            public string ru { get; set; }
            public string lv { get; set; }
            public string zh { get; set; }
            public string kn { get; set; }
            public string fr { get; set; }
        }
    }
}

