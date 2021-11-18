## "Current City Weather Comparison" console app :sun_behind_rain_cloud:
### CodeLouisville .NET Project
This is a Console Application that can run on Windows, Linux and macOS
NET 5.0 (Current)
You can compare current weather in up to 5 cities at a time. Cities will be sorted from warmest to coldest in the output.
I am using IHttpClientFatory to consume multiple API calls in a for loop.

### Description:
- To access current weather data for any location on Earth, I am using OpenWeather Api https://openweathermap.org/current
- Api call is done by City name and 2-letter Country code. Example: 'http://api.openweathermap.org/data/2.5/weather?q=,FR&appid={API key}'
- For US locations, it is recommended to call by City name, State code, and Country code. i.e., there are 27 places named Madison in US.
You have to specify the state in this case. Example: 'http://api.openweathermap.org/data/2.5/weather?q=Madison,IN,US&appid={API key}'
- For temperature in Fahrenheit use &units=imperial. For temperature in Celsius use &units=metric. Default is in Kelvin.
- OpenWeather uses UTC time zone for all API calls.

### Instructions:
1. Create a free OpenWeatherMap account https://home.openweathermap.org/users/sign_up and get your API key. It is FREE, but you need to register.
2. Create an environment variable named OpenWeatherAPI and put your API key as the value.
3. Open Visual Studio and load WeatherComparison solution.
4. To use IHttpClientFatory, you need to add "Microsoft.Extensions.Http" nuget package.
5. Run the appliction.
6. Enter cities in the following format, i.e.,
   - Paris,FR
   - Louisville,KY,US -> for US locations

### File Descriptions
coming soon

### Notes
Below is the list of CodeLouisville .Net Project Requirements that were fulfilled.
1. Implement a "master loop" console application where the user can repeatedly enter commands/perform actions.
2. Create a class, then create at least one object of that class and populate it with data.
3. Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program.
4. Create and call at least 3 functions or methods, at least one of which must return a value that is used somewhere else in your code.
5. Connect to an external/3rd party API and read data into your app.

#### Thank you for reading! :sun_with_face:


