using System;
using System.Collections.Generic;

namespace Api_Wave.Models;

public partial class EstadoSala
{
    public int IdEstadosala { get; set; }

    public int Estado { get; set; }

    public int IdIntegrante { get; set; }

    public virtual IntegrantesSala IdIntegranteNavigation { get; set; } = null!;
}
