using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class EstadoMensaje
{
    public int IdEstado { get; set; }

    public string NombreEstado { get; set; } = null!;

    public int IdMensaje { get; set; }

    public int? IdTipoSala { get; set; }

    public virtual ICollection<DetalleEstadoMensaje> DetalleEstadoMensajes { get; set; } = new List<DetalleEstadoMensaje>();

    public virtual Mensaje IdMensajeNavigation { get; set; } = null!;
}
