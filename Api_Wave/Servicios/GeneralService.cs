using Api_Wave.Models;
using Api_Wave.Models.ViewModels;

namespace Api_Wave.Servicios
{
    public class GeneralService:IGeneralService
    {
        private readonly ChatwaveContext milinq;
        public GeneralService(ChatwaveContext _milinq)
        {
            this.milinq = _milinq;
        }
        //obtener datos para las fichas de los mensjes
        public ModelDatossalas datossalas(string idsala, string idpersona)
        {
          
            var dato2 =( from f in milinq.IntegrantesSalas
                        join m in milinq.Mensajes on f.IdSala equals m.IdSala
                        where f.IdSala == idsala
                        orderby m.FechaMensaje descending
                        select new
                        {
                            nombresala = f.IdSalaNavigation.NombreSala,
                            ultimome = /*m.Imagen.ToString() ??*/ m.Mensaje1 ?? m.Archivo.ToString() ?? m.Audio.ToString(),//selecciona el que no sea null
                            fecha = m.FechaMensaje.ToString("d/M/yyyy"),
                            hora = m.FechaMensaje.ToString("hh:mm tt"),
                            tipochat=f.IdSalaNavigation.IdTipoSala,
                            envia=m.IdIntegranteNavigation.IdPersona == idpersona ? true : false//true si es la misma persona que envio el ultimo mensaje, false si es otra persona que lo envio
                        }).FirstOrDefault();
            ModelDatossalas md = new ModelDatossalas { 
                nombresala=dato2.nombresala,
                ultimomensaje=dato2.ultimome,
                fecha=dato2.fecha,
                hora=dato2.hora,
                envia=dato2.envia
            };

            if(dato2.tipochat==1)
            {
                var idcontacto = (from f in milinq.IntegrantesSalas
                                  where f.IdSala == idsala && f.IdPersona != idpersona
                                  select new
                                  {
                                      idpersona = f.IdPersona
                                  }).FirstOrDefault();
                md.nombresala = idcontacto.idpersona;
            }

            return md;
                        
        }
        //obtiene solo el listado de los salas con sus codigos
        public List<ModelMPrincipal> inicio(string idpersona)
        {
            var salas =( from s in milinq.Salas
                        join p in milinq.IntegrantesSalas on s.IdSala equals p.IdSala
                        join m in milinq.Mensajes on s.IdSala equals m.IdSala
                        where p.IdPersona == idpersona && s.EstadoChat == true
                        orderby m.FechaMensaje ascending
                        group s.IdSala by new ModelMPrincipal
                        {
                            id_sala=s.IdSala
                        }
                        into gru
                        select new ModelMPrincipal
                        {
                           id_sala=gru.Key.id_sala
                        }).ToList();
            return salas;

        }
        
    }
}
