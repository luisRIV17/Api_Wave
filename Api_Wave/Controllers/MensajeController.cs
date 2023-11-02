using Api_Wave.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api_Wave.Models.ViewModels;

namespace Api_Wave.Controllers
{
    [Route("mensaje")]
    [ApiController]
    public class MensajeController : ControllerBase
    {
        private readonly IMensajeService men;
        public MensajeController(IMensajeService _men)
        {
            this.men = _men;
        }
        [HttpGet]
        [Route("listmen")]
        public List<ModelMensaje> cargamensaje(string idsala, int idintegrante)
        {
            return men.cargamensaje(idsala,idintegrante);
        }

        [HttpPost]
        [Route("insert")]
        public bool ingresomensaje(ModelIngresoMensaje ingre)
        {
            return men.enviarmensaje(ingre);
        }
        [HttpPut]
        [Route("srecibido")]
        public bool acutestadorecibi(string idper)
        {
            return men.actualizarestadosRecibido(idper);
        }
        [HttpPut]
        [Route("sleido")]
        public bool acutestadoleido(int idinte, string idsala)
        {
            return men.actualizarestadoLeido(idinte,idsala);
        }
    }
}
