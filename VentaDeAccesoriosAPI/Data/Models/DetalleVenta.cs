using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class DetalleVenta
{
    public int IdDetalle { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
    [JsonIgnore]
    public virtual Venta? IdVentaNavigation { get; set; }
}
