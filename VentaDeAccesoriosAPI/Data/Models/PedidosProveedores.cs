using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class PedidosProveedores
{
    public int IdPedido { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdUsuario { get; set; }

    public DateOnly? FechaPedido { get; set; }

    public string? EstadoPedido { get; set; }

    public virtual ICollection<DetallePedidosProveedores> DetallePedidosProveedores { get; set; } = new List<DetallePedidosProveedores>();

    public virtual ICollection<FacturasCompra> FacturasCompras { get; set; } = new List<FacturasCompra>();

    public virtual Proveedores? IdProveedorNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
