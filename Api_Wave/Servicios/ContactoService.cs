using Api_Wave.Models;
using Api_Wave.Models.ViewModels.Contacto;

namespace Api_Wave.Servicios
{
    public class ContactoService : IContactoService
    {

        private readonly ChatwaveContext milinq;
        public ContactoService(ChatwaveContext _milinq)
        {
            this.milinq = _milinq;
        }

        public List<ModelContacto> listcontac(string idpersona)
        {
           var contacto = from c in milinq.Contactos
                          join usu in milinq.Usuarios on c.IdUsuario equals usu.IdUsuario
                          join pu in milinq.PersonaUsuarios on usu.IdUsuario equals pu.IdUsuario
                          where pu.IdPersona == idpersona
                          select new ModelContacto
                          {
                              nombrecontacto = c.AliasContacto ?? (from p in milinq.Personas where p.IdPersona == c.UsuarioContacto select p.Nombre).FirstOrDefault() ?? "Sin valor",
                              leyenda = (from p in milinq.Personas where p.IdPersona == c.UsuarioContacto select p.Leyenda).FirstOrDefault(),
                              idpersona=c.UsuarioContacto,
                          };
            return contacto.ToList();
        }
    }
}
