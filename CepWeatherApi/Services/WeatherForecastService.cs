using CepWeatherApi.Data;
using CepWeatherApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    public  Weather FindById(int id)
    {
        var result = _context.Weather.FirstOrDefault(p => p.Id == id);
        return result;
    }

    public async Task InsertAsync(Weather weather)
    {
        _context.Add(weather);
        await _context.SaveChangesAsync();
    }
    internal async Task Update(Weather weather)
    {
        bool hasAny = await _context.Weather.AnyAsync(x => x.Id == weather.Id);
        if(!hasAny)
        {
            throw new KeyNotFoundException("Id not found");
        } 
        try
        {
            _context.Update(weather);
            await _context.SaveChangesAsync();

        } catch(DBConcurrencyException e)
        {
            throw new DBConcurrencyException("Erro de DbConcurrency " + e.Message);
        }
    }

    public async void Delete(Weather weather)
    {
        _context.Remove(weather);
        _context.SaveChanges();
    } 
    [HttpPost]
    public async Task<string> GetWeatherForecast(double latitude, double longitude, string timezone, DateTime inicio, DateTime fim)
    {
        string inicioData = inicio.ToString("yyyy-MM-dd");
        string fimData = fim.ToString("yyyy-MM-dd");

        string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=" +
            $"temperature_2m&timezone={timezone}&start_date={inicioData}&end_date={fimData}";

        var response = await _httpClient.GetStringAsync(apiUrl);
        return response;
    }

   
}
