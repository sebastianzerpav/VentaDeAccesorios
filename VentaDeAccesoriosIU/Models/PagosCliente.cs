using System;
using System.Collections.Generic;



public partial class PagosCliente
{
    public int IdPago { get; set; }

    public int? IdVenta { get; set; }

    public DateOnly? FechaPago { get; set; }

    public decimal? MontoPagado { get; set; }

    public string? MedioPago { get; set; }

    public string? Estado { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
