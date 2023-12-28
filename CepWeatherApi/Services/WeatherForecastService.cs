using System;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherForecastService
{
    private readonly HttpClient httpClient;

    public WeatherForecastService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<string> GetWeatherForecast(double latitude, double longitude)
    {

        string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={1.1}&longitude={1.1}&hourly=temperature_2m";
        var response = await httpClient.GetStringAsync(apiUrl);

        return response;
    }
}
