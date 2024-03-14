using Api_Wave.Models;
using Api_Wave.Models.ViewModels;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Linq;


namespace Api_Wave.Servicios
{
    public class PersonaService:IPersonaService
    {
        private readonly ChatwaveContext milinq;
        public PersonaService(ChatwaveContext _milinq)
        {
            this.milinq = _milinq;
        }
        public string crearperona()
        {
            string idper = DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString();
            var id= (from f in milinq.Personas
                     where f.IdPersona.StartsWith(idper)
                    orderby f.IdPersona descending
                    select f).FirstOrDefault();
            if (id == null)
                idper = idper + "1001";
            else
                idper=(Convert.ToInt32( id.IdPersona)+1).ToString();
            return idper;
        }
        public string insertapersona(ModelIngresapersona per)
        {
            //try
            //{
            string token = crearperona();
            var nuevaPersona = new Persona
            {
                IdPersona = token,
                Nombre = per.Nombre,
                Apellido = per.Apellido,
                Correo = per.Correo,
                Telefono = per.Telefono,
                FechaNac=per.FechaNac,
                Leyenda="Disponible"
            };
                milinq.Personas.Add(nuevaPersona);
            milinq.SaveChanges();
            var nuevousuario = new Usuario
            {
                Nombre= per.Correo,
                Contraseña=per.contra,
                Estado=true
            };
            milinq.Usuarios.Add(nuevousuario);
            milinq.SaveChanges();
            var nuevousuper = new PersonaUsuario
            {
                Fecha=DateTime.Now,
                IdUsuario=nuevousuario.IdUsuario,
                IdPersona=nuevaPersona.IdPersona
            };
          milinq.PersonaUsuarios.Add(nuevousuper);
            milinq.SaveChanges();
            return token;
            //}
            //catch {  return false; }
        }
        public string iniciarSesion(string email, string contraseña)
        {
            var query = (from data in milinq.Personas
                         join dt in milinq.PersonaUsuarios on data.IdPersona equals dt.IdPersona
                         join user in milinq.Usuarios on dt.IdUsuario equals user.IdUsuario
                         where data.Correo.Equals(email) && user.Contraseña.Equals(contraseña)
                         select data.IdPersona).FirstOrDefault();

            if (query != null) // Comprobamos si query es diferente de 0 (o cualquier otro valor predeterminado en caso de no coincidencia)
            {
                return query.ToString();
            }
            else
            {
                return "Usuario no encontrado";
            }
        }

        public ModelPersona listper(string idpersona)
        {
            var persona = (from data in milinq.Personas
                          where data.IdPersona == idpersona
                          select new ModelPersona
                          {
                              idpersona = data.IdPersona,
                              nombre = data.Nombre,
                              apellido = data.Apellido,
                              tel = data.Telefono,
                              correo = data.Correo,
                              leyenda = data.Leyenda

                          }).FirstOrDefault();
            return persona;

                        
            
        }

        public bool actualizaleyenda(ModelNuevaleyenda nueva)
        {
            var nueavleyend =( from p in milinq.Personas
                              where p.IdPersona == nueva.idpersona
                               select p).ToList();
            foreach(var f in nueavleyend)
            {
                f.Leyenda = nueva.leyenda;
            }
            milinq.SaveChanges();
            return true;
        }
    }
}
