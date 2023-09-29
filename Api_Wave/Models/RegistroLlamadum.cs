using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class RegistroLlamadum
{
    public int IdRegistroLlamada { get; set; }

    public DateTime FechaLlamada { get; set; }

    public string Duracion { get; set; } = null!;

    public int IdIntegrante { get; set; }

    public virtual IntegrantesSala IdIntegranteNavigation { get; set; } = null!;
}
