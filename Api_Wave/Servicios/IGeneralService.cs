using Api_Wave.Models;
using Api_Wave.Models.ViewModels;
namespace Api_Wave.Servicios
{
    public interface IGeneralService
    {
        public List<ModelMPrincipal> inicio(string idpersona);
        public ModelDatossalas datossalas(string idsala, string idpersona);
    }
}
