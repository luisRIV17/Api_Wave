using Api_Wave.Models;

using Api_Wave.Models.ViewModels;
using System.Collections.Generic;

namespace Api_Wave.Servicios
{
    public class MensajeService:IMensajeService
    {
        private readonly ChatwaveContext milinq;
        public MensajeService(ChatwaveContext _milinq)
        {
            this.milinq = _milinq;
        }

        public List<ModelMensaje> cargamensaje(string idsala,int idintegrante )
        {
            var cargamen = from c in milinq.Mensajes
                           where c.IdSala == idsala 
                           select new ModelMensaje
                           {
                               idpersona = c.IdIntegranteNavigation.IdPersona,
                               nombrepersona = c.IdIntegranteNavigation.IdPersonaNavigation.Nombre + " " + c.IdIntegranteNavigation.IdPersonaNavigation.Apellido,
                               mensaje = /*m.Imagen.ToString() ??*/ c.Mensaje1 ?? c.Archivo.ToString() ?? c.Audio.ToString(),
                               fecha = c.FechaMensaje.ToString("d/M/yyyy"),
                               hora = c.FechaMensaje.ToString("hh:mm tt"),
                              
                               //quienenvia=c.IdIntegrante==idintegrante?true
                           };

            int par = 0;
            return cargamen.ToList();
        }

       
        public bool enviarmensaje(ModelIngresoMensaje dat)
        {
            var nuevomensaje = new Mensaje
            {
                EstadoMensaje = true,
                FechaMensaje=DateTime.Now,
                Archivo = dat.Archivo,
                Imagen = dat.Imagen,
                Audio = dat.Audio,
                Mensaje1=dat.Mensaje1,
                IdIntegrante = dat.IdIntegrante,
                IdSala = dat.IdSala,
            };
            milinq.Mensajes.Add(nuevomensaje);
            milinq.SaveChanges();



            var nuevoestamen = new EstadoMensaje
            {
                NombreEstado="sin leer",
                IdMensaje=nuevomensaje.IdMensaje,
                IdTipoSala=dat.tiposala
            };

            milinq.EstadoMensajes.Add(nuevoestamen);
            milinq.SaveChanges();

            var integra= from i in milinq.IntegrantesSalas
                         where i.IdSala == dat.IdSala && i.IdIntegrante != dat.IdIntegrante
                         select i;
            if(integra.Count() ==1)
            {
                var nuevodeta = new DetalleEstadoMensaje
                {
                    FechaEstadoDet = null,
                    IdIntegrante = integra.FirstOrDefault().IdIntegrante,
                    IdTipoLectura = 1,
                    IdEstado = nuevoestamen.IdEstado
                };
                milinq.DetalleEstadoMensajes.Add (nuevodeta);
                milinq.SaveChanges ();
            }
            else
            {
                //en caso de ser grupal
            }
           
            return true;
        }
    }
}
