using Api_Wave.Models;
using Api_Wave.Models.ViewModels;
namespace Api_Wave.Servicios
{
    public interface IPersonaService
    {
      
        public string insertapersona(ModelIngresapersona per);
        public string iniciarSesion(string email, string contraseña);
    }
}
