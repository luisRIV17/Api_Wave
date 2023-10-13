using Api_Wave.Models;
using Api_Wave.Models.ViewModels;
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
            string idper = "";
            var id= (from f in milinq.Personas
                    orderby f.IdPersona descending
                    select f).FirstOrDefault();
            if (id == null)
                idper = "1001";
            else
                idper=(Convert.ToInt32( id.IdPersona)+1).ToString();
            return idper;
        }
        public bool insertapersona(ModelIngresapersona per)
        {
            //try
            //{
            var nuevaPersona = new Persona
            {
                IdPersona = crearperona(),
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
            return true;
            //}
            //catch {  return false; }
        }
    }
}
