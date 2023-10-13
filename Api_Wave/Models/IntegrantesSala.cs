using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class IntegrantesSala
{
    public int IdIntegrante { get; set; }

    public bool EstadoIntegrante { get; set; }

    public bool EstadoAdministrador { get; set; }

    public string IdPersona { get; set; } = null!;

    public string IdSala { get; set; } = null!;

    public virtual ICollection<DetalleEstadoMensaje> DetalleEstadoMensajes { get; set; } = new List<DetalleEstadoMensaje>();

    public virtual ICollection<EstadoSala> EstadoSalas { get; set; } = new List<EstadoSala>();

    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    public virtual Sala IdSalaNavigation { get; set; } = null!;

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    public virtual ICollection<RegistroLlamadum> RegistroLlamada { get; set; } = new List<RegistroLlamadum>();
}
