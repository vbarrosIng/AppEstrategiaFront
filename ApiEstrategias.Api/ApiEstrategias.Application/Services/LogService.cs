using ApiEstrategias.Application.Interfaces;
using ApiEstrategias.Domain.Entities;
using ApiEstrategias.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Application.Services
{
    public class LogService : ILogService
    {
        public LogService(ILogsRepository logsRepository)
        {
            LogsRepository = logsRepository;
        }

        public ILogsRepository LogsRepository { get; }


        //Funcion que inserta los logs
        public void InsertLogS(long EstrategiaId, string Usuario, long PilotoId)
        {
            Logs logs = new Logs();
            logs.FechaRegistro = DateTime.Now;
            logs.EstrategiaId = EstrategiaId;
            logs.Usuario = Usuario;
            logs.PilotoId = PilotoId;

            LogsRepository.InsertLog(logs);
        }
    }
}
