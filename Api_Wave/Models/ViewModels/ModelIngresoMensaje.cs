namespace Api_Wave.Models.ViewModels
{
    public class ModelIngresoMensaje
    {


        public string? Mensaje1 { get; set; } = null;

        public string? Archivo { get; set; } = null;

        public byte[]? Imagen { get; set; } = null;

        public byte[]? Audio { get; set; } = null;

        public int IdIntegrante { get; set; }

        public string? IdSala { get; set; }

        public int tiposala { get; set; }
    }
}
