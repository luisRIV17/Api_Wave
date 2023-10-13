using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class TipoLectura
{
    public int IdTipoLectura { get; set; }

    public string? NombreTipoLectura { get; set; }

    public virtual ICollection<DetalleEstadoMensaje> DetalleEstadoMensajes { get; set; } = new List<DetalleEstadoMensaje>();
}
