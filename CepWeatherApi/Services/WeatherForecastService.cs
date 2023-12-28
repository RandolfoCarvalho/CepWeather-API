using CepWeatherApi.Data;
using CepWeatherApi.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;
    private readonly CepWeatherApiContext _context;
    public WeatherForecastService(HttpClient httpClient, CepWeatherApiContext context)
    {
        _httpClient = httpClient;
        _context = context;

    }
    public async Task InsertAsync(Weather weather)
    {
        _context.Add(weather);
        await _context.SaveChangesAsync();
    }
    public async Task<string> GetWeatherForecast(double latitude, double longitude)
    {
        string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={1.1}&longitude={1.1}&hourly=temperature_2m&timezone=America/Sao_Paulo&start_date=2023-12-28&end_date=2023-12-28";
        var response = await _httpClient.GetStringAsync(apiUrl);
        return response;
    }
}
