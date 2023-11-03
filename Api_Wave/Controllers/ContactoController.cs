using Api_Wave.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_Wave.Models.ViewModels.Contacto;

namespace Api_Wave.Controllers
{
    [Route("contacto")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoService cont;
        public ContactoController(IContactoService _cont)
        {
            this.cont = _cont;
        }
        [HttpGet]
        [Route("listmen")]
        public List<ModelContacto> listcontac(string idpersona)
        {
            return cont.listcontac(idpersona);
        }
        [HttpPost]
        [Route("insert")]
        public bool creacontanto(ModelCreaconta nuevoconta)
        {
            return cont.creacontacto(nuevoconta);
        }
    }
}
