using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class PagosProveedores
{
    public int IdPago { get; set; }

    public int? IdFactura { get; set; }

    public DateOnly? FechaPago { get; set; }

    public decimal? MontoPagado { get; set; }

    public string? MedioPago { get; set; }

    public virtual FacturasCompra? IdFacturaNavigation { get; set; }
}
