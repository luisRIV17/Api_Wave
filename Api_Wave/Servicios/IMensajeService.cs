﻿using Api_Wave.Models;
using Api_Wave.Models.ViewModels;
namespace Api_Wave.Servicios
{
    public interface IMensajeService
    {
        public List<ModelMensaje> cargamensaje(string idsala, int idintegrante);

        public bool enviarmensaje(ModelIngresoMensaje dat);
    }
}
