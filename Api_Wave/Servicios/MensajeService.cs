using Api_Wave.Models;

using Api_Wave.Models.ViewModels;
using System.Collections.Generic;

namespace Api_Wave.Servicios
{
    public class MensajeService : IMensajeService
    {
        private readonly ChatwaveContext milinq;
        public MensajeService(ChatwaveContext _milinq)
        {
            this.milinq = _milinq;
        }
        public List<ModelMensaje> cargamensaje(string idsala,int idintegrante )
        {
            var cargamen = from c in milinq.Mensajes
                           join e in milinq.EstadoMensajes on c.IdMensaje equals e.IdMensaje
                           where c.IdSala == idsala 
                           orderby c.FechaMensaje ascending
                           select new ModelMensaje
                           {
                               idmen=c.IdMensaje,
                               idintegrante = c.IdIntegranteNavigation.IdIntegrante,
                               nombrepersona = 
                               (from ci in milinq.IntegrantesSalas
                                join p in milinq.PersonaUsuarios on ci.IdPersona equals p.IdPersona
                                join con in milinq.Contactos on p.IdUsuario equals con.IdUsuario
                                where ci.IdSala == idsala && ci.IdIntegrante==idintegrante && con.UsuarioContacto == c.IdIntegranteNavigation.IdPersona
                                select  con.AliasContacto
                                ).FirstOrDefault()??
                               c.IdIntegranteNavigation.IdPersonaNavigation.Nombre + " " + c.IdIntegranteNavigation.IdPersonaNavigation.Apellido,
                               mensaje = /*m.Imagen.ToString() ??*/ c.Mensaje1 ?? c.Archivo.ToString() ?? c.Audio.ToString(),
                               fecha = c.FechaMensaje.ToString("d/M/yyyy"),
                               hora = c.FechaMensaje.ToString("hh:mm tt"),
                              estadolecturamen=e.NombreEstado,
                              estadoquienleyo=c.IdIntegrante==idintegrante?"1":"2",
                              tipogrupo= c.IdSalaNavigation.IdTipoSala
                               
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
        #region Estados
        public bool actualizarestadosRecibido(string idpersona)
        {
            var salaidinte =( from s in milinq.IntegrantesSalas
                             where s.IdPersona == idpersona
                             select new
                             {
                                 idinte = s.IdIntegrante,
                                 idsala = s.IdSala
                             }).ToList();
            foreach( var f in salaidinte)
            {
                var mensajes = (from m in milinq.Mensajes
                               join e in milinq.EstadoMensajes on m.IdMensaje equals e.IdMensaje
                               where m.IdSala == f.idsala && e.NombreEstado == "sin leer" && m.IdIntegrante != f.idinte
                               select e).ToList();
                var mensajes2 = (from m in milinq.Mensajes
                                join e in milinq.EstadoMensajes on m.IdMensaje equals e.IdMensaje
                                join d in milinq.DetalleEstadoMensajes on e.IdEstado equals d.IdEstado
                                where m.IdSala == f.idsala && e.NombreEstado == "sin leer" && m.IdIntegrante != f.idinte
                                select d).ToList();
                foreach (var mensaje in mensajes)
                {
                    mensaje.NombreEstado = "recibido";
                }
                foreach (var mensajes2s in mensajes2)
                {
                    mensajes2s.IdTipoLectura = 2;
                    mensajes2s.FechaEstadoDet=DateTime.Now;
                }

                milinq.SaveChanges();
            }


            return true;
        }

        public bool actualizarestadoLeido(int codintegrante, string idsala)
        {
         
            var mensajes = (from m in milinq.Mensajes
                            join e in milinq.EstadoMensajes on m.IdMensaje equals e.IdMensaje
                            where m.IdSala == idsala && m.IdIntegrante != codintegrante
                            select e).ToList();
            var mensajes2 = (from m in milinq.Mensajes
                             join e in milinq.EstadoMensajes on m.IdMensaje equals e.IdMensaje
                             join d in milinq.DetalleEstadoMensajes on e.IdEstado equals d.IdEstado
                             where m.IdSala == idsala && m.IdIntegrante != codintegrante
                             select d).ToList();
            foreach (var mensaje in mensajes)
            {
                mensaje.NombreEstado = "leido";
            }
            foreach (var mensajes2s in mensajes2)
            {
                mensajes2s.IdTipoLectura = 4;
                mensajes2s.FechaEstadoDet = DateTime.Now;
            }
            milinq.SaveChanges();
            return true;

        }
        #endregion

    }




}
