using Api_Wave.Models.ViewModels.Contacto;
namespace Api_Wave.Servicios
   
{
    public interface IContactoService
    {
        public List<ModelContacto> listcontac(string idpersona);

        public bool creacontacto(ModelCreaconta nuevo);
    }
}
