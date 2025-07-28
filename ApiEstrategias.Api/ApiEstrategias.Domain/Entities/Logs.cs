using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Domain.Entities
{
    public class Logs
    {
        public long Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Usuario { get; set; }
        public long PilotoId { get; set; }
        public long EstrategiaId { get; set; }
    }
}
