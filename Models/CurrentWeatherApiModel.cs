﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherComparison.Models
{
	// used "json2csharp" Online Converter to generate C# Classes from currentweatherapi-response-example.json
	public class CurrentWeatherApiModel
	{
		[JsonPropertyName("coord")]
		public Coord Coord { get; set; }

		[JsonPropertyName("weather")]
		public List<Weather> Weather { get; set; }

		[JsonPropertyName("base")]
		public string Base { get; set; }

		[JsonPropertyName("main")]
		public MainConditions Main { get; set; }

		[JsonPropertyName("visibility")]
		public int Visibility { get; set; }

		[JsonPropertyName("wind")]
		public Wind Wind { get; set; }

		[JsonPropertyName("clouds")]
		public Clouds Clouds { get; set; }

		[JsonPropertyName("dt")]
		public int Dt { get; set; }

		[JsonPropertyName("sys")]
		public Sys Sys { get; set; }

		[JsonPropertyName("timezone")]
		public int Timezone { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("cod")]
		public int Cod { get; set; }
	}


	public class Coord
	{
		[JsonPropertyName("lon")]
		public double Lon { get; set; }

		[JsonPropertyName("lat")]
		public double Lat { get; set; }
	}

	public class Weather
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("main")]
		public string Main { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("icon")]
		public string Icon { get; set; }
	}

	public class MainConditions
	{
		[JsonPropertyName("temp")]
		public double Temp { get; set; }

		[JsonPropertyName("feels_like")]
		public double FeelsLike { get; set; }

		[JsonPropertyName("temp_min")]
		public double TempMin { get; set; }

		[JsonPropertyName("temp_max")]
		public double TempMax { get; set; }

		[JsonPropertyName("pressure")]
		public int Pressure { get; set; }

		[JsonPropertyName("humidity")]
		public int Humidity { get; set; }
	}

	public class Wind
	{
		[JsonPropertyName("speed")]
		public double Speed { get; set; }

		[JsonPropertyName("deg")]
		public int Deg { get; set; }
	}

	public class Clouds
	{
		[JsonPropertyName("all")]
		public int All { get; set; }
	}

	public class Sys
	{
		[JsonPropertyName("type")]
		public int Type { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("message")]
		public double Message { get; set; }

		[JsonPropertyName("country")]
		public string Country { get; set; }

		[JsonPropertyName("sunrise")]
		public int Sunrise { get; set; }

		[JsonPropertyName("sunset")]
		public int Sunset { get; set; }
	}
}