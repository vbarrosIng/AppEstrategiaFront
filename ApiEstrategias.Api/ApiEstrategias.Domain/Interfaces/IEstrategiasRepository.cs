using ApiEstrategias.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Domain.Interfaces
{
    public interface IEstrategiasRepository
    {
        Task<long> InsertarEstrategia(Estrategias estrategias);
        Task<List<DetalleEstrategia>> InsertDetalleEstrategia(List<DetalleEstrategia> detalleEstrategias);
    }
}
