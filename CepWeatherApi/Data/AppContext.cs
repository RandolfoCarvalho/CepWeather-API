using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CepWeatherApi.Models;

namespace CepWeatherApi.Data
{
    public class CepWeatherApiContext : DbContext
    {
        public CepWeatherApiContext(DbContextOptions<CepWeatherApiContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;port=3306;Connect Timeout=5;",
                new MySqlServerVersion(new Version(8, 0, 100))
            );
        }
        // Declare DbSet para o modelo CepModel
        public DbSet<Cep> Cep { get; set; }
    }
}
