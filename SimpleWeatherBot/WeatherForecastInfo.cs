using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWeatherBot
{
    public class WeatherForecastInfo
    {
        public city city { get; set; }
        public List<list> list { get; set; }
        public string cod { get; set; }
        public int cnt { get; set; }

    }

    public class list
    {
        public double dt { get; set; } // day in milli second
        public string dt_txt { get; set; }
        public main main { get; set; }
        public List<weather> weather { get; set; }
        public clouds clouds { get; set; }
        public rain rain { get; set; }
        public wind wind { get; set; }
        public sys sys { get; set; }

    }
    public class rain
    {
        public double h {get;set;}// rain drops mm in 3 hours
    }
    public class clouds
    {
        public double all { get; set; }// percentage of clouds in sky
    }
    public class city
    {
        public int id { get; set; }
        public string name { get; set; }
        public coord coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public int timezone { get; set; }
        public double sunrise { get; set; }
        public double sunset { get; set; }
    }
    
}
