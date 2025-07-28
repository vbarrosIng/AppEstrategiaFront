using ApiEstrategias.Application.DTOs;
using ApiEstrategias.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Application.Interfaces
{
    public interface IEstrategiaService
    {
        //Task<List<List<Neumaticos>>> GenerarEstrategias(int CantidadMV, long IdPiloto, string Usuario);
        Task<List<DTOEstrategia>> GenerarEstrategias(int CantidadMV, long IdPiloto, string Usuario);
    }
}
