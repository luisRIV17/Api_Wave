using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class Sala
{
    public string IdSala { get; set; } = null!;

    public bool EstadoChat { get; set; }

    public DateTime FechaIncio { get; set; }

    public int IdTipoSala { get; set; }

    public string? NombreSala { get; set; }

    public virtual TipoSala IdTipoSalaNavigation { get; set; } = null!;

    public virtual ICollection<IntegrantesSala> IntegrantesSalas { get; set; } = new List<IntegrantesSala>();

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();
}
