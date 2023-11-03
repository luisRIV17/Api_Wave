using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_Wave.Servicios;
using Api_Wave.Models;
using Api_Wave.Models.ViewModels;

namespace Api_Wave.Controllers
{
    [Route("persona")]
    [ApiController]

    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService per;
        public PersonaController(IPersonaService _per)
        {
            this.per = _per;
        }
        [HttpGet]
        [Route("getCodigo")]
        public string getCodigo(string email, string password)
        {
            return per.iniciarSesion(email, password);
        }
        [HttpPost]
        [Route("insert")]
        public string insertapersona(ModelIngresapersona pers)
        {

            return per.insertapersona(pers);
        }

    }
}
