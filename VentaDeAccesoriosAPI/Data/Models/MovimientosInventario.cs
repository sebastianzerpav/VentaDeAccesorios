using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class MovimientosInventario
{
    public int IdMovimiento { get; set; }

    public int? IdProducto { get; set; }

    public int? IdSedeOrigen { get; set; }

    public int? IdSedeDestino { get; set; }

    public int? Cantidad { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Motivo { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Sede? IdSedeDestinoNavigation { get; set; }

    public virtual Sede? IdSedeOrigenNavigation { get; set; }
}
