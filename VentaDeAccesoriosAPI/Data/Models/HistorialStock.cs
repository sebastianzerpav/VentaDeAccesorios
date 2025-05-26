using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class HistorialStock
{
    public int IdRegistro { get; set; }

    public int? IdProducto { get; set; }

    public int? IdSede { get; set; }

    public DateOnly? FechaCambio { get; set; }

    public int? StockAnterior { get; set; }

    public int? StockActual { get; set; }

    public string? Causa { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Sede? IdSedeNavigation { get; set; }
}
