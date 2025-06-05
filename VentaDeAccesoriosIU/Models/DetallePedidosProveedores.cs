using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


public partial class DetallePedidosProveedores
{
    public int IdDetalle { get; set; }

    public int? IdPedido { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }
    [JsonIgnore]
    public virtual PedidosProveedores? IdPedidoNavigation { get; set; }
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
}
