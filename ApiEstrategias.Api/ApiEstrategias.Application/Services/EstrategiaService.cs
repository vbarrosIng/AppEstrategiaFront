using ApiEstrategias.Application.DTOs;
using ApiEstrategias.Application.Interfaces;
using ApiEstrategias.Domain.Entities;
using ApiEstrategias.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstrategias.Application.Services
{
    public class EstrategiaService : IEstrategiaService
    {
        public EstrategiaService(IEstrategiasRepository estrategiasRepository, INeumaticoRepository neumaticoRepository,
            IPilotoRepository pilotoRepository, ILogService logService)
        {
            EstrategiasRepository = estrategiasRepository;
            NeumaticoRepository = neumaticoRepository;
            PilotoRepository = pilotoRepository;
            LogService = logService;

        }

        public IEstrategiasRepository EstrategiasRepository { get; }
        public INeumaticoRepository NeumaticoRepository { get; }
        public IPilotoRepository PilotoRepository { get; }
        public ILogService LogService { get; }
        
        //Funcion que maneja la logica de las estrategias y devuelve el resultado al controlador
        public async Task<List<DTOEstrategia>> GenerarEstrategias(int CantidadMV, long IdPiloto, string Usuario)
        {
            try
            {

                List<List<Neumaticos>> Estrategias = new List<List<Neumaticos>>();
                List<DTOEstrategia> setEstrategias = new List<DTOEstrategia>();

                var ResObtenerNeumaticos = await NeumaticoRepository.GetNeumaticos();
                if (ResObtenerNeumaticos == null)
                {
                    throw new Exception("No se pudieron obtener los neumaticos");
                }

                var ListaNeumaticosA = ResObtenerNeumaticos;
                var listNeumaticosB = ResObtenerNeumaticos;

                //Se iteran los neumaticos para poder aplicar la logica de combinacion de estrategias
                foreach (var itemLNA in ListaNeumaticosA)
                {

                    foreach (var itemLNB in listNeumaticosB)
                    {
                        List<Neumaticos> ListaEstrategias = new List<Neumaticos>();
                        ListaEstrategias.Add(itemLNA);
                        var arrayInicial = (itemLNA.VueltasEstimadas, ListaEstrategias);

                        var ResEstrategiaGenerada = IteradorSubNeumaticos(arrayInicial, itemLNB, CantidadMV);
                        Estrategias.Add(ResEstrategiaGenerada);


                    }
                }

                //Se recorren las estrategias generadas para poder realizar la insercion de estas
                //en base de datos
                foreach (var itemEG in Estrategias)
                {
                    Estrategias estragiasGeneradas = new Estrategias();

                    var TotalVuetlas = itemEG.Sum(i => i.VueltasEstimadas);
                    var TotalRendimiento = itemEG.Sum(i => i.Rendimiento);
                    var TotalConsumo = itemEG.Sum(i => i.ConsumoPorVuelta);

                    List<string> listDetalleEstrategia = itemEG.Select(i => i.Tipo).ToList();

                    estragiasGeneradas.PilotoId = IdPiloto;
                    estragiasGeneradas.TotalVueltas = TotalVuetlas;
                    estragiasGeneradas.TotalConsumo = TotalConsumo;
                    estragiasGeneradas.TotalRendimiento = TotalRendimiento;
                    estragiasGeneradas.Fecha = DateTime.Now;

                    //Se insertan las estrategias generadas entro de la tabla principal
                    var ResEstrategiaInsert = await InsertarEstrategia(estragiasGeneradas);

                    //Se insertan los logs de las estrategias generadass
                    LogService.InsertLogS(ResEstrategiaInsert, Usuario, IdPiloto);

                    //Se inserta el detalle de las estrategias generadas
                    //Lista tipos de neumaticos utilizados para la estrategia
                    await InsertarDetalleEstrategia(ResEstrategiaInsert, listDetalleEstrategia);

                }


                var ResPiloto = await GetPilotoById(IdPiloto);
                var setResPiloto = "";
                if (ResPiloto == null) { setResPiloto = ""; } else { setResPiloto = ResPiloto.Nombre; };
                
                //Se crea el dto que sera entregado al controlador
                foreach (var itemES in Estrategias)
                {
                    DTOEstrategia dTOEstrategia = new DTOEstrategia();
                    dTOEstrategia.TotalRendimiento = itemES.Sum(i => i.Rendimiento);
                    dTOEstrategia.TotalVueltas = itemES.Sum(i => i.VueltasEstimadas);
                    dTOEstrategia.TotalConsumo = itemES.Sum(i => i.ConsumoPorVuelta);
                    dTOEstrategia.PromedioConsumo = (decimal) dTOEstrategia.TotalConsumo / dTOEstrategia.TotalVueltas;
                    dTOEstrategia.NombrePiloto = setResPiloto;

                    dTOEstrategia.ListEstrategia = itemES.Select(i => i.Tipo).ToList();
                    setEstrategias.Add(dTOEstrategia);
                }

                var orderSetEstrategia = setEstrategias.OrderBy(s => s.PromedioConsumo).ToList();

                return orderSetEstrategia;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pilotos?> GetPilotoById(long IdPiloto) {
            var ResPiloto = await PilotoRepository.GetpiloyoById(IdPiloto);
            if (ResPiloto == null) {
                return null;
            }
            return ResPiloto;
        }

        //Funcion que crea las estrategias en cada una de sus iteraciones
        //Tiene el cuenta el neumatico iterado y devuelve una lista con los neumaticos para las 
        //combinaciones de las estrategias
        public List<Neumaticos> IteradorSubNeumaticos((int, List<Neumaticos>) arrayInicial, Neumaticos itemIterado, int CantidadMV)
        {
            try
            {
                List<Neumaticos> ListEstrategiasSub = new List<Neumaticos>(arrayInicial.Item2);
                var TotalVueltas = arrayInicial.Item1;


                while (TotalVueltas <= CantidadMV)
                {
                    TotalVueltas += itemIterado.VueltasEstimadas;

                    //Se valida que el total de las vueltas de los neumaticos iterados no supere el total 
                    //de la cantidad de vueltas que se ingresa
                    if (TotalVueltas <= CantidadMV)
                    {

                        ListEstrategiasSub.Add(itemIterado);
                    }
                }

                return ListEstrategiasSub;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Funcion que inserta las estrategias en base de datos
        public async Task<long> InsertarEstrategia(Estrategias estrategias)
        {
            try
            {
                var ResEstratgiaInsertada = await EstrategiasRepository.InsertarEstrategia(estrategias);
                return ResEstratgiaInsertada;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Funcion que inserta el detalle de cada estrategia en la base de datos
        //NOTA: Inicialmente no se menciona en las tablas propuestas en la solucion, pero se considera
        //necesaria para el ejercicio
        public async Task InsertarDetalleEstrategia(long IdEstrategia, List<string> DetalleTipoEstratgia)
        {

            try
            {
                List<DetalleEstrategia> ListDetalleEstrategiaAInsert = new List<DetalleEstrategia>();
                foreach (var itemDE in DetalleTipoEstratgia)
                {
                    DetalleEstrategia detalleEstrategia = new DetalleEstrategia();

                    detalleEstrategia.TipoNeumatico = itemDE;
                    detalleEstrategia.IdEstrategia = IdEstrategia;

                    ListDetalleEstrategiaAInsert.Add(detalleEstrategia);
                }

                await EstrategiasRepository.InsertDetalleEstrategia(ListDetalleEstrategiaAInsert);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
