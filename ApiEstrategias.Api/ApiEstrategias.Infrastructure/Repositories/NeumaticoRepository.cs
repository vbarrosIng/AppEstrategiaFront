using ApiEstrategias.Domain.Entities;
using ApiEstrategias.Domain.Interfaces;
using ApiEstrategias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Infrastructure.Repositories
{
    public class NeumaticoRepository : INeumaticoRepository
    {
        public NeumaticoRepository(AppDbContext appDbContext) {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }

        public async Task<List<Neumaticos>> GetNeumaticos() {
            return await AppDbContext.Neumaticos.ToListAsync();
        }

    }
}
