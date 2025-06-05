using System;
using System.Collections.Generic;



public partial class Reclamo
{
    public int IdReclamo { get; set; }

    public int? IdCliente { get; set; }

    public int? IdVenta { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? FechaReclamo { get; set; }

    public string? Estado { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
