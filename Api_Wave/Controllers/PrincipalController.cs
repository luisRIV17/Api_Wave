using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_Wave.Servicios;
using Api_Wave.Models;
using Api_Wave.Models.ViewModels;

namespace Api_Wave.Controllers
{

    [Route("inicio")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        private readonly IGeneralService gen;
        public PrincipalController(IGeneralService _gen)
        {
            this.gen = _gen;
        }
        [HttpGet]
        [Route("listsalas")]
        public List<ModelMPrincipal> cargasalas(string idpersona)
        {
            return gen.inicio(idpersona);
        }
        [HttpGet]
        [Route("listdetamen")]
        public ModelDatossalas cargardetamen(string idsala, string idpersona)
        {
            return gen.datossalas(idsala, idpersona);
        }
        [HttpPost]
        [Route("insertsala")]
        public ModelMPrincipal insertasala( ModelSalaNueva dat)
        {
            return gen.crearnuevasala(dat);
        }
        [HttpPost]
        [Route("insertsalagrupal")]
        public ModelMPrincipal insertasalag(ModelSalanuevaGrupal dat)
        {
            return gen.crearnuevasalagrupo(dat);
        }
    }
}
