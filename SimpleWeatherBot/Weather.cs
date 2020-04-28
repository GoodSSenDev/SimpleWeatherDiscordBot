using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace SimpleWeatherBot
{
    class Weather
    {
        
        static readonly string AppID = "7569edd6eb60612b7dd63174529c5a58";

        private WeatherInfo DownloadCurrentInfo(string url)
        {
            WeatherInfo output;
            using (WebClient web = new WebClient())
            {
                try
                { 
                    var json = web.DownloadString(url);
                    output = JsonConvert.DeserializeObject<WeatherInfo>(json);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{e} ERROR {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                    return null;
                }
            }

            return output; 
        }
        private WeatherForecastInfo DownloadForecastInfo(string url)
        {
            WeatherForecastInfo output;
            using (WebClient web = new WebClient())
            {
                try
                {
                    var json = web.DownloadString(url);
                    output = JsonConvert.DeserializeObject<WeatherForecastInfo>(json);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{e} ERROR {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                    return null;
                }
            }

            return output;
        }


        public async Task<WeatherInfo> GetCurrentWeatherAsync(string city)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={AppID}";
            WeatherInfo result = null;

            result = await Task.Run(() => DownloadCurrentInfo(url));

            return result;
        }
        public async Task<WeatherInfo> GetCurrentWeatherAsync(string city, string countryCode)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={city},{countryCode}&units=metric&appid={AppID}";
            WeatherInfo result = null;

            try
            {
                result = await Task.Run(() => DownloadCurrentInfo(url));
            }
            catch (Exception e)
            {

                Debug.WriteLine($"{e} ERROR {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

            return result;
        }
        public async Task<WeatherForecastInfo> GetForecastWeatherAsync(string city, string countryCode)
        {
            string url = $"http://api.openweathermap.org/data/2.5/forecast?q={city},{countryCode}&units=metric&appid={AppID}";
            WeatherForecastInfo result = null;

            try
            {
                result = await Task.Run(() => DownloadForecastInfo(url));
            }
            catch (Exception e)
            {

                Debug.WriteLine($"{e} ERROR {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

            return result;
        }
        public string GetCurrentWeather(string city)
        {
            string currentWeatherInfo = "";
            using (WebClient web = new WebClient())
            {
                string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={AppID}";
                var json = web.DownloadString(url);

                WeatherInfo output = JsonConvert.DeserializeObject<WeatherInfo>(json);

                currentWeatherInfo = $"Current Weather: {output.weather[0].main}\n" +
                    $"On {output.name}\n" +
                    $"Current temperature:{output.main.temp} Min: {output.main.temp_min} Max: {output.main.temp_max}\n" +
                    $"Pressure: {output.main.pressure}\n" +
                    $"Wind {output.wind.speed}\n" +
                    $"Now it is {output.weather[0].description}\n";

            }

            return currentWeatherInfo;
        }
    }
}
