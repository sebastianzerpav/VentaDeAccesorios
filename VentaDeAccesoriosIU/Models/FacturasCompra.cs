using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;



public partial class FacturasCompra
{
    public int IdFactura { get; set; }

    public int? IdPedido { get; set; }

    public DateOnly? FechaFactura { get; set; }

    public decimal? Total { get; set; }

    public string? Moneda { get; set; }

    public string? MetodoPago { get; set; }
    [JsonIgnore]
    public virtual PedidosProveedores? IdPedidoNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<PagosProveedores> PagosProveedores { get; set; } = new List<PagosProveedores>();
}
