using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Domain.Entities
{
    public class Pilotos
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Equipo { get; set; }
        public string Nacionalidad { get; set; }
    }
}
