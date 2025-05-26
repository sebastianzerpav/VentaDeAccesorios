using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? PrecioCompra { get; set; }

    public decimal? PrecioVenta { get; set; }

    public int? StockTotal { get; set; }

    public int? GarantiaMeses { get; set; }

    public virtual ICollection<ComentariosCliente> ComentariosClientes { get; set; } = new List<ComentariosCliente>();

    public virtual ICollection<DetallePedidosProveedores> DetallePedidosProveedores { get; set; } = new List<DetallePedidosProveedores>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual ICollection<Garantia> Garantia { get; set; } = new List<Garantia>();

    public virtual ICollection<HistorialStock> HistorialStocks { get; set; } = new List<HistorialStock>();

    public virtual ICollection<MovimientosInventario> MovimientosInventarios { get; set; } = new List<MovimientosInventario>();

    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();

    public virtual ICollection<ProductosProveedores> ProductosProveedores { get; set; } = new List<ProductosProveedores>();
}
