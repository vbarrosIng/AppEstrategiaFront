using ApiEstrategias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
            
        }

        public DbSet<Pilotos> Pilotos { get; set; }
        public DbSet<Estrategias> Estrategias { get; set; }
        public DbSet<Neumaticos> Neumaticos { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<DetalleEstrategia> DetalleEstrategia { get; set; }

    }
}
