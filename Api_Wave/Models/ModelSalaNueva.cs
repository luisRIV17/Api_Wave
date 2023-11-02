using Api_Wave.Models.ViewModels;

namespace Api_Wave.Models
{
    public class ModelSalaNueva
    {
        public string idpersonacreo { get; set; }

        public bool EstadoChat { get; set; }

        public int IdTipoSala { get; set; }

        public string? NombreSala { get; set; }


        public string idpersonaconta { get; set; }
    }
}
