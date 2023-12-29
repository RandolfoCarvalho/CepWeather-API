using CepWeatherApi.Data;
using CepWeatherApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    [HttpGet]
    public async Task<string> GetWeatherForecast(double latitude, double longitude, string timezone, DateTime inicio, DateTime fim)
    {
        string inicioData = inicio.ToString("yyyy-MM-dd");
        string fimData = fim.ToString("yyyy-MM-dd");

        string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=" +
            $"temperature_2m&timezone=America/Denver&start_date={inicioData}&end_date={fimData}";
        var response = await _httpClient.GetStringAsync(apiUrl);
        return response;
    }
}
