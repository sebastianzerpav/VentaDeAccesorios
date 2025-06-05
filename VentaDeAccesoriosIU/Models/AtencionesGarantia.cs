using System;
using System.Collections.Generic;
public partial class AtencionesGarantia
{
    public int IdAtencion { get; set; }

    public int? IdGarantia { get; set; }

    public DateOnly? FechaAtencion { get; set; }

    public string? DescripcionSolucion { get; set; }

    public string? Resultado { get; set; }

    public virtual Garantia? IdGarantiaNavigation { get; set; }
}
