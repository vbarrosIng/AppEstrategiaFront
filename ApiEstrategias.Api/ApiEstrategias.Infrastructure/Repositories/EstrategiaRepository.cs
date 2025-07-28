using ApiEstrategias.Domain.Entities;
using ApiEstrategias.Domain.Interfaces;
using ApiEstrategias.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Infrastructure.Repositories
{
    public class EstrategiaRepository : IEstrategiasRepository
    {
        public EstrategiaRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }


        public async Task<long> InsertarEstrategia(Estrategias estrategias)
        {
            try {

                await AppDbContext.Estrategias.AddAsync(estrategias);
                await AppDbContext.SaveChangesAsync();

                return estrategias.Id;

            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }

        }

        public async Task<List<DetalleEstrategia>> InsertDetalleEstrategia(List<DetalleEstrategia> detalleEstrategias)
        {

            await AppDbContext.AddRangeAsync(detalleEstrategias);
            await AppDbContext.SaveChangesAsync();
            return detalleEstrategias.ToList();

        }
    }
}
