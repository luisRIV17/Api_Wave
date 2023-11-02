namespace Api_Wave.Models.ViewModels
{
    public class ModelMensajeGrupo
    {
        public string idpersona { get; set; }
        public string nombrepersona { get; set; }
        public string mensaje { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public bool estaorecibido { get; set; }
        public bool quienenvia { get; set; }
    }
}
