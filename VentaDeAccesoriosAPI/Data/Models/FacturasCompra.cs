using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class FacturasCompra
{
    public int IdFactura { get; set; }

    public int? IdPedido { get; set; }

    public DateOnly? FechaFactura { get; set; }

    public decimal? Total { get; set; }

    public string? Moneda { get; set; }

    public string? MetodoPago { get; set; }

    public virtual PedidosProveedores? IdPedidoNavigation { get; set; }

    public virtual ICollection<PagosProveedores> PagosProveedores { get; set; } = new List<PagosProveedores>();
}
