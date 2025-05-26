using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Garantia
{
    public int IdGarantia { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<AtencionesGarantia> AtencionesGarantia { get; set; } = new List<AtencionesGarantia>();

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
