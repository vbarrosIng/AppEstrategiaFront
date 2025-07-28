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
    public class LogsRepository : ILogsRepository
    {
        public LogsRepository(AppDbContext appDbContext) {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }

        public void InsertLog(Logs logs) {
            AppDbContext.Logs.Add(logs);
            AppDbContext.SaveChanges();

        }
    }
}
