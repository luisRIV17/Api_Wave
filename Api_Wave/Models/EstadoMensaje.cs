using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class EstadoMensaje
{
    public int IdEstado { get; set; }

    public string NombreEstado { get; set; } = null!;

    public int IdMensaje { get; set; }

    public int IdIntegrante { get; set; }

    public virtual IntegrantesSala IdIntegranteNavigation { get; set; } = null!;

    public virtual Mensaje IdMensajeNavigation { get; set; } = null!;
}
