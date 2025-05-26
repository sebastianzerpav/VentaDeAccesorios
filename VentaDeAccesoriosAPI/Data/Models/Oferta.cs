using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Oferta
{
    public int IdOferta { get; set; }

    public int? IdProducto { get; set; }

    public decimal? PrecioPromocional { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
