using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class Mensaje
{
    public int IdMensaje { get; set; }

    public bool EstadoMensaje { get; set; }

    public DateTime FechaMensaje { get; set; }

    public string? Mensaje1 { get; set; }

    public string? Archivo { get; set; }

    public byte[]? Imagen { get; set; }

    public byte[]? Audio { get; set; }

    public int IdIntegrante { get; set; }

    public string? IdSala { get; set; }

    public virtual ICollection<EstadoMensaje> EstadoMensajes { get; set; } = new List<EstadoMensaje>();

    public virtual IntegrantesSala IdIntegranteNavigation { get; set; } = null!;

    public virtual Sala? IdSalaNavigation { get; set; }
}
