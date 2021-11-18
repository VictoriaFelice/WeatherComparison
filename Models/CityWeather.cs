using System;

namespace WeatherComparison.Models
{
    class CityWeather
    {
        public string City { get; }
        public string CountryCode { get; }
        public int UnixTimeOfDataCapture { get; }
        public int Timezone { get; } // shift in seconds from UTC
        public double Temperature { get; }
        public string WeatherDescription { get; }
        public int Humidity { get; }

        public CityWeather(string city, string countryCode, int unixTimeOfDataCapture, int timezone,
            double temperature, string weatherDescription,
            int humidity)
        {
            City = city;
            CountryCode = countryCode;
            UnixTimeOfDataCapture = unixTimeOfDataCapture;
            Timezone = timezone;
            Temperature = temperature;
            WeatherDescription = weatherDescription;
            Humidity = humidity;
        }

        public static void PrintCityWeather(CityWeather city)
        {
            var emoji = GetEmoji(city.WeatherDescription);

            Console.WriteLine(city.City.ToUpper());
            Console.WriteLine("Local time: " + GetLocalTime(city.UnixTimeOfDataCapture, city.Timezone));
            Console.WriteLine($"Temperature: {city.Temperature}");
            Console.WriteLine($"Weather description: {city.WeatherDescription} " + emoji);
            Console.WriteLine($"Humidity: {city.Humidity}%");
        }

        private static string GetLocalTime(int unixTimeOfDataCollection, int timezone)
        {
            var localTime = unixTimeOfDataCollection + timezone;
            var dateTimeOffSet = DateTimeOffset.FromUnixTimeSeconds(localTime);
            var localDateTime = dateTimeOffSet.DateTime;

            return $"{localDateTime:f}";
        }

        private static string GetEmoji(string weatherDescription)
        {
            return weatherDescription switch
            {
                { } s when s.Contains("clear") => "☺", //"\x263A",
                { } s when s.Contains("cloud") => "≈",
                { } s when s.Contains("rain") => "‴",
                { } s when s.Contains("snow") => "✶",
                { } s when s.Contains("sun") => "☼", //\u263C
                { } s when s.Contains("thunderstorm") => "♯",
                _ => "☻"
            };
        }
    }
}