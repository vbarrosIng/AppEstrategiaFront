using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Domain.Entities
{
    public class Neumaticos
    {
        [Key]
        public long Id { get; set; }
        public string Tipo { get; set; }
        public int VueltasEstimadas { get; set; }
        public decimal ConsumoPorVuelta { get; set; }
        public int Rendimiento { get; set; }
    }
}

