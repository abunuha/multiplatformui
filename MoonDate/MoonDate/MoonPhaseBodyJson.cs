using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonDate
{
    public class MoonPhaseBodyJson
    {
        public static StringContent GetJson()
        {
            Observer observer = new Observer()
            {
                latitude = 1,
                longitude = 1,
                date = "",
            };

            

            String dateStr = DateTime.Now.ToString("yyyy-MM-dd");


            Root root = new Root()
            {
                format = "png",
                style = new Style()
                {
                    moonStyle = "sketch",
                    backgroundStyle = "stars",
                    backgroundColor = "red",
                    headingColor = "white",
                    textColor = "red",
                },
                observer = new Observer()
                {
                    latitude = 6.56774,
                    longitude = 79.88956,
                    date = dateStr,
                },
                view = new View()
                {
                    type = "portrait-simple",
                    orientation = "south-up",
                },
            };

            Style style = new Style()
            {
                moonStyle = "",
                backgroundStyle = "",
                backgroundColor = "",
                headingColor = "",
                textColor = "",
            };

            View view = new View()
            {
                type = "",
                orientation = "",
            };

            String json = JsonConvert.SerializeObject(root);
            return new StringContent(json);

        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Observer
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string date { get; set; }
        }

        public class Root
        {
            public string format { get; set; }
            public Style style { get; set; }
            public Observer observer { get; set; }
            public View view { get; set; }
        }

        public class Style
        {
            public string moonStyle { get; set; }
            public string backgroundStyle { get; set; }
            public string backgroundColor { get; set; }
            public string headingColor { get; set; }
            public string textColor { get; set; }
        }

        public class View
        {
            public string type { get; set; }
            public string orientation { get; set; }
        }


    }
}
