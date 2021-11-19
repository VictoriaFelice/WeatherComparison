using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WeatherComparison.Models;

namespace WeatherComparison
{
    public static class Program
    {
        private const int DecorationMainLength = 80;

        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode; // this will allow weather emoji to show up in console
            PrintWelcomeMessage();
            while (true)
            {
                var city = "";
                var numberOfCitiesEntered = 0;
                const int maxCities = 5;
                var cities = new List<string>();
                while (numberOfCitiesEntered < maxCities && city.ToLower() != "d" && city.ToLower() != "q")
                {
                    city = ReadCity(numberOfCitiesEntered);
                    if (city.ToLower() == "d") continue;
                    if (cities.Contains(city))
                    {
                        Console.WriteLine($"'{city}' has already been entered. Please, enter a different city.");
                        continue;
                    }

                    cities.Add(city);
                    numberOfCitiesEntered += 1;
                }

                var weather = await CurrentWeatherToDictionary(cities);
                if (weather != null)
                {
                    var sortedCites = SortCitiesByTemp(weather);
                    OutputResults(sortedCites);

                    Console.Write("\nDo you want to enter new cities? Press Enter to continue or type 'Q' to quit: ");
                    var yesNo = Console.ReadLine();
                    if (yesNo?.ToLower() != "q") continue;
                    return;
                }
            }
        }

        private static string ReadCity(int numberOfCitiesEntered)
        {
            switch (numberOfCitiesEntered)
            {
                case 0:
                    Console.Write(
                        "Enter City,two-letter Country Code i.e [Venice,IT]\nFor US locations - Enter City,State,US i.e [Louisville,KY,US]: ");
                    break;
                case 1:
                    Console.Write(
                        "\nEnter 2nd city in the same format. You can compare up to 5 cities: ");
                    break;
                default:
                    Console.Write(
                        $"Enter {numberOfCitiesEntered + 1} city (or type 'D' if you are done entering): ");
                    break;
            }

            var cityWithCountryCode = Console.ReadLine();
            while (string.IsNullOrEmpty(cityWithCountryCode) ||
                   cityWithCountryCode != "d" && cityWithCountryCode.Count(c => c == ',') == 0)
            {
                Console.WriteLine("Invalid input. Enter comma separated values, i.e Paris,FR: ");
                cityWithCountryCode = Console.ReadLine();
            }

            return cityWithCountryCode;
        }

        private static async Task<Dictionary<string, CityWeather>> CurrentWeatherToDictionary(
            IEnumerable<string> citesWithCountries)
        {
            var currentWeatherDict = new Dictionary<string, CityWeather>();
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            foreach (var cityCountry in citesWithCountries)
            {
                try
                {
                    // CreateClient() gets the instance of http client from pool if exists, if not creates a new object
                    var httpClient = httpClientFactory?.CreateClient();

                    var apiUrl =
                        $"http://api.openweathermap.org/data/2.5/weather?q={cityCountry}&appid={Environment.GetEnvironmentVariable("OpenWeatherAPI")}&units=imperial";
                    var response = await httpClient?.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var res = JsonSerializer.Deserialize<CurrentWeatherApiModel>(content);
                    var cityWeather = new CityWeather(
                        res.Name,
                        res.Sys.Country,
                        res.Dt,
                        res.Timezone,
                        res.Main.Temp,
                        res.Weather[0].Description,
                        res.Main.Humidity);

                    // TODO: ensure unique keys in Dictionary
                    currentWeatherDict[cityCountry] = cityWeather;
                }
                catch (HttpRequestException httpRequestException)
                {
                    Console.WriteLine("\n");
                    PrintErrorFiglet();
                    Console.WriteLine($"Error getting weather data from OpenWeatherMap: {httpRequestException.Message}");
                    if (httpRequestException.StatusCode == null) return default;
                    switch ((int)httpRequestException.StatusCode)
                    {
                        case 401:
                            Console.WriteLine("Please, check your API key.\n");
                            break;
                        case 404:
                            Console.WriteLine($"City you entered '{cityCountry}' not found.\n");
                            break;
                        default:
                            Console.WriteLine("Unexpected error. Please, try again.");
                            break;
                    }

                    return default;
                }
            }

            return currentWeatherDict;
        }

        private static Dictionary<string, CityWeather> SortCitiesByTemp(
            Dictionary<string, CityWeather> currentWeatherDict)
        {
            var sortedCities = currentWeatherDict.OrderByDescending(kv => kv.Value.Temperature).Distinct()
                .ToDictionary(k => k.Key, v => v.Value);
            return sortedCities;
        }

        private static void OutputResults(Dictionary<string, CityWeather> sortedCityWeather)
        {
            PrintResultsHeading(sortedCityWeather);
            foreach (var city in sortedCityWeather)
            {
                CityWeather.PrintCityWeather(city.Value);
                PrintDecorationLine('=', DecorationMainLength);
            }
        }
        
        // Printing
        private static void PrintWelcomeMessage()
        {
            // const string welcomeMessage = "Welcome to the \"Current City Weather Comparison\" program!";
            // var spacingBefore = new string(' ', (DecorationMainLength - welcomeMessage.Length) / 2);
            // PrintDecorationLine('*', DecorationMainLength);
            //Console.WriteLine(spacingBefore + welcomeMessage);
            PrintWelcomeFiglet();
            PrintDecorationLine('=', DecorationMainLength);
        }

        private static void PrintDecorationLine(char element, int length)
        {
            Console.WriteLine(new string(element, length));
        }

        private static void PrintResultsHeading(Dictionary<string, CityWeather> sortedCityWeather)
        {
            var resultMessage = 
                $"Currently, the warmest city is {sortedCityWeather.Keys.First().ToUpper()}. \nColdest city is {sortedCityWeather.Keys.Last().ToUpper()}.";
            var resultDecorationLine = new StringBuilder("+-".Length * DecorationMainLength / 2).Insert(0, "+-", DecorationMainLength / 2).ToString();
            
            Console.WriteLine("\n");
            PrintResultsFiglet();
            PrintDecorationLine('=', DecorationMainLength);
            Console.WriteLine("\n" + resultDecorationLine + "\n");
            Console.WriteLine(resultMessage);
            Console.WriteLine("\n" + resultDecorationLine + "\n");
            PrintDecorationLine('=', DecorationMainLength);
        }

        private static void PrintWelcomeFiglet()
        {
            Console.WriteLine(@" _              _");
            Console.WriteLine(@"(_|   |   |_ / | |");
            Console.WriteLine(@"  |   |   | _  | |  __   __   _  _  _    _");
            Console.WriteLine(@"  |   |   ||/  |/  /    /  \_/ |/ |/ |  |/");
            Console.WriteLine(@"   \_/ \_/ |__/|__/\___/\__/   |  |  |_/|__//");
        }

        private static void PrintResultsFiglet()
        {
            Console.WriteLine(@" , __                  _");     
            Console.WriteLine(@"/|/  \                | |");
            Console.WriteLine(@" |___/  _   ,         | |_|_  ,");
            Console.WriteLine(@" | \   |/  / \_|   |  |/  |  / \_");
            Console.WriteLine(@" |  \_/|__/ \/  \_/|_/|__/|_/ \/");
        }

        private static void PrintErrorFiglet()
        {
            Console.WriteLine(@" ___");
            Console.WriteLine(@"/ (_)");                   
            Console.WriteLine(@"\__   ,_    ,_    __   ,_");
            Console.WriteLine(@"/    /  |  /  |  /  \_/  |");
            Console.WriteLine(@"\___/   |_/   |_/\__/    |_/");
        }
    }
}