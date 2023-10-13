using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class DetalleEstadoMensaje
{
    public int IdDetalleEstado { get; set; }

    public DateTime? FechaEstadoDet { get; set; }

    public int IdIntegrante { get; set; }

    public int? IdTipoLectura { get; set; }

    public int? IdEstado { get; set; }

    public virtual EstadoMensaje? IdEstadoNavigation { get; set; }

    public virtual IntegrantesSala IdIntegranteNavigation { get; set; } = null!;

    public virtual TipoLectura? IdTipoLecturaNavigation { get; set; }
}
