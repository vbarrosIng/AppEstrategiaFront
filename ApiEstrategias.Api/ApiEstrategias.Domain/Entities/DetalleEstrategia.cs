using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Domain.Entities
{
    public class DetalleEstrategia
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Piloto")]
        public long IdEstrategia { get; set; }
        public Pilotos Piloto { get; set; }
        public string TipoNeumatico { get; set; }
    }
}
