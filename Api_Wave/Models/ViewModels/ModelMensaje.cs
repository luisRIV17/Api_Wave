namespace Api_Wave.Models.ViewModels
{
    public class ModelMensaje
    {
        public int idmen { get; set; }
        public int idintegrante { get; set; }
        public string nombrepersona { get; set; }
        public string mensaje { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string estadoquienleyo { get; set; }
        public string estadolecturamen {  get; set; }
        public bool estadomensaje {  get; set; }
        public int tipogrupo { get; set; }
    }
}
