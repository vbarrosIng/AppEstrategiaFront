using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Application.Interfaces
{
    public interface ILogService
    {
        void InsertLogS(long EstrategiaId, string Usuario, long PilotoId);
    }
}
