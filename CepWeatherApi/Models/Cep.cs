using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CepWeatherApi.Data;

namespace CepWeatherApi.Models
{
    public class Cep
    {
        public string Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        // Construtor padrão
         public Cep(string id, string bairro, string estado, string logradouro, string cidade) 
        {
            this.Id = id;
            this.Bairro = bairro;
            this.Estado = estado;
            this.Logradouro = logradouro;
            this.Cidade = cidade;
        }
    }
}