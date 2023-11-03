namespace Api_Wave.Models.ViewModels
{
    public class ModelSalanuevaGrupal
    {
        public string idpersonacreo { get; set; }

        public bool EstadoChat { get; set; }

        public int IdTipoSala { get; set; }

        public string? NombreSala { get; set; }

        public List<ModelIntegranteNuevo> idpersonaconta { get; set; }
    }
}
