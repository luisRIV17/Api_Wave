using Api_Wave.Models;

using Api_Wave.Models.ViewModels;

namespace Api_Wave.Servicios
{
    public class MensajeService:IMensajeService
    {
        private readonly ChatwaveContext milinq;
        public MensajeService(ChatwaveContext _milinq)
        {
            this.milinq = _milinq;
        }

        public List<ModelMensaje> cargamensaje(string idsala)
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

                           };
            return cargamen.ToList();
        }
    }
}
