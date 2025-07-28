using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Domain.Entities
{
    public class Estrategias
    {
        [Key]
        public long Id { get; set; }
        public long PilotoId { get; set; }
        public DateTime Fecha { get; set; }
        public int TotalVueltas { get; set; }
        public int TotalRendimiento { get; set; }
        public decimal TotalConsumo { get; set; }
    }
}
