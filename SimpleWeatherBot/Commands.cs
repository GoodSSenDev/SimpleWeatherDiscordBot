using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace SimpleWeatherBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task ping()
        {
            await ReplyAsync("cw [City Name] :> current weather for one of city" +
                "cw [City Name] [Country Code eg. US EU NZ] :> current weather for one of city (but specific country)" +
                "hw [City Name] [Country Code eg. US EU NZ] :> 5 days of Weather forecast (3hours each)");
        }

        [Command("cw")]
        public async Task CurrentWeather(params string[] inputArray)
        {// first string in the inputArray will be name of City and Second string is countryCode

            Weather weather = new Weather();
            WeatherInfo weatherInfo = null;
            if (inputArray.Length == 1)
            {
                string city = inputArray[0];
                weatherInfo = await weather.GetCurrentWeatherAsync(city);
            }
            if (inputArray.Length == 2)
            {
                string city = inputArray[0];
                string countryCode = inputArray[1];
                weatherInfo = await weather.GetCurrentWeatherAsync(city, countryCode);
            }

            if (weatherInfo == null) // if getting weatherInfo method getting exception than weatherinfo would be null
                await ReplyAsync("Error Occur Try again ^오^");
            else
            {
                

                string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\");
                var embed = new EmbedBuilder();

                embed.ThumbnailUrl = $"attachment://{weatherInfo.weather[0].icon}.png";
                embed.Title = $"{ weatherInfo.weather[0].main }";
                embed.Description = $"On {weatherInfo.name}\n" +
                    $"Current temperature:{weatherInfo.main.temp} \nMin: {weatherInfo.main.temp_min} Max: {weatherInfo.main.temp_max}\n" +
                    $"Pressure: {weatherInfo.main.pressure}\n" +
                    $"Wind {weatherInfo.wind.speed}\n" +
                    $"Now it is {weatherInfo.weather[0].description}";


                await Context.Channel.SendFileAsync($"{filepath}{weatherInfo.weather[0].icon}.png","",false, embed.Build());

                //await ReplyAsync($"Current Weather: {weatherInfo.weather[0].main}\n" +
                //    $"On {weatherInfo.name}\n" +
                //    $"Current temperature:{weatherInfo.main.temp} Min: {weatherInfo.main.temp_min} Max: {weatherInfo.main.temp_max}\n" +
                //    $"Pressure: {weatherInfo.main.pressure}\n" +
                //    $"Wind {weatherInfo.wind.speed}\n" +
                //    $"Now it is {weatherInfo.weather[0].description}");
            }
            
        }

        [Command("hw")]
        public async Task HourlyForecastWeather(params string[] inputArray)
        {

            if (inputArray.Length != 2)
            {
                await ReplyAsync("Wrong input try again +.+");
                return;
            }
            Weather weather = new Weather();
            WeatherForecastInfo weatherForecast = null;

            string city = inputArray[0];
            string countryCode = inputArray[1];

            weatherForecast = await weather.GetForecastWeatherAsync(city, countryCode);

            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\");

            if (weatherForecast == null) // if getting weatherInfo method getting exception than weatherinfo would be null
                await ReplyAsync("Error Occur Try again ^오^");
            else
            {
                await ReplyAsync($"Weather Forecast 5days 3hours each " +
                    $"On {weatherForecast.city.name} Country: {weatherForecast.city.country}\n");


                for (int i = 0; i < weatherForecast.list.Count; i++)
                {
                    var embed = new EmbedBuilder();
                    embed.ThumbnailUrl = $"attachment://{weatherForecast.list[i].weather[0].icon}.png";
                    embed.Title = $"\n{weatherForecast.list[i].weather[0].main}\n";
                    embed.Description = $"\n{weatherForecast.list[i].dt_txt}\n" +
                         $"Temperature(Celsius): {weatherForecast.list[i].main.temp} \nMin: {weatherForecast.list[i].main.temp_min} Max: {weatherForecast.list[i].main.temp_max}\n" +
                         $"Clouds(%): {weatherForecast.list[i].clouds.all}\n" +
                         $"Weather Forecast: {weatherForecast.list[i].weather[0].description}\n" +
                         ((weatherForecast.list[i].rain == null) ? "" : $"rain: {weatherForecast.list[i].rain.h} mm");


                    await Context.Channel.SendFileAsync($"{filepath}{weatherForecast.list[i].weather[0].icon}.png", "", false, embed.Build());
                    //await ReplyAsync($"\n{weatherForecast.list[i].dt_txt}\n" +
                    //   $"Temperature(Celsius): {weatherForecast.list[i].main.temp} Min: {weatherForecast.list[i].main.temp_min} Max: {weatherForecast.list[i].main.temp_max}\n" +
                    //   $"Clouds(%): {weatherForecast.list[i].clouds.all}\n" +
                    //   $"Weather Forecast: {weatherForecast.list[i].weather[0].description}\n" +
                    //   ((weatherForecast.list[i].rain == null) ? "" : $"rain: {weatherForecast.list[i].rain.h} mm")
                    //   + "\n\n");
                }
            }
        }

        [Command("name")]
        public async Task name()
        {
            await ReplyAsync("hello im Simple Weather Bot");
        }



    }
}
