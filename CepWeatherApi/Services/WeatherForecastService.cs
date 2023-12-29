using CepWeatherApi.Data;
using CepWeatherApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;
    private readonly CepWeatherApiContext _context;
    public WeatherForecastService(HttpClient httpClient, CepWeatherApiContext context)
    {
        _httpClient = httpClient;
        _context = context;

    }
    public List<Weather> FindAll()
    {
        return _context.Weather.ToList();
    }
    public Weather FindById(long id)
    {
        var result = _context.Weather.FirstOrDefault(p => p.Id == id);
        return result;
    }

    public async Task InsertAsync(Weather weather)
    {
        _context.Add(weather);
        await _context.SaveChangesAsync();
    }

   /* public string ToJson()
    {

    } */
    

    [HttpGet]
    public async Task<string> GetWeatherForecast(double latitude, double longitude, string timezone, DateTime inicio, DateTime fim)
    {
        string inicioData = inicio.ToString("yyyy-MM-dd");
        string fimData = fim.ToString("yyyy-MM-dd");

        string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=" +
            $"temperature_2m&timezone=America/Sao_Paulo&start_date=2023-07-01&end_date=2023-07-02";
        var response = await _httpClient.GetStringAsync(apiUrl);
        return response;
    }
}
