using ApiEstrategias.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Application.DTOs
{
    public class DTOEstrategia
    {
        public int TotalVueltas { get; set; }
        public int TotalRendimiento { get; set; }
        public decimal TotalConsumo { get; set; }
        public decimal PromedioConsumo { get; set; }
        public string NombrePiloto { get; set; }
        public List<string> ListEstrategia { get; set; }
        
    }
}
