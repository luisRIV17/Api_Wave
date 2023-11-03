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

        public bool creacontacto(ModelCreaconta nuevo)
        {
            var persona = (from p in milinq.Personas
                          where p.Telefono ==nuevo.Telefono && p.Correo==nuevo.Correo
                          select p.IdPersona).FirstOrDefault();
            if(persona==null)
            {
                return false;
            }
            else
            {
                var conta = from f in milinq.Contactos
                            join u in milinq.Usuarios on f.IdUsuario equals u.IdUsuario
                            join pu in milinq.PersonaUsuarios on u.IdUsuario equals pu.IdUsuario
                            where f.UsuarioContacto==persona && pu.IdPersona== nuevo.idpersona
                            select f;
                var usuario =( from u in milinq.Usuarios
                              join p in milinq.PersonaUsuarios on u.IdUsuario equals p.IdUsuario
                              where p.IdPersona == nuevo.idpersona
                              select p.IdUsuario).FirstOrDefault();
                if(conta.Count()==0)
                {
                    var nuvocontacto = new Contacto
                    {
                        UsuarioContacto = persona,
                        EstadoContacto = true,
                        AliasContacto = nuevo.nombrealias,
                        Fecha = DateTime.Now,
                        IdUsuario = usuario
                    };
                    milinq.Contactos.Add(nuvocontacto);
                    milinq.SaveChanges();   
                    return true;
                }
                else
                {
                    return false;
                }
            }
            

           
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
