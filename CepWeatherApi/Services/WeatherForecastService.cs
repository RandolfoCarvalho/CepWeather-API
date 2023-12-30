using CepWeatherApi.Data;
using CepWeatherApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
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

    /*public async Task<Weather> FindById(int id)
    {
        //da um join para buscar também o departamento pelo Id
        //isso é chamado de eager loading, carregar outros objetos associados ao obj principal, que no caso o princial 
        return await _context.Weather.FirstOrDefaultAsync(obj => obj.Id == id);
    } */
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

    [HttpPost]
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
