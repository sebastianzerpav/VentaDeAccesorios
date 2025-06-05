using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;



public partial class PedidosProveedores
{
    public int IdPedido { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdUsuario { get; set; }

    public DateOnly? FechaPedido { get; set; }

    public string? EstadoPedido { get; set; }

    [JsonIgnore] 
    public virtual ICollection<DetallePedidosProveedores> DetallePedidosProveedores { get; set; } = new List<DetallePedidosProveedores>();

    [JsonIgnore] 
    public virtual ICollection<FacturasCompra> FacturasCompras { get; set; } = new List<FacturasCompra>();

    [JsonIgnore] 
    public virtual Proveedores? IdProveedorNavigation { get; set; }

    [JsonIgnore] 
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
