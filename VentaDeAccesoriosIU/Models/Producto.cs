using System.Collections.Generic;
using System.Text.Json.Serialization;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal? PrecioCompra { get; set; }

    public decimal? PrecioVenta { get; set; }

    public int? StockTotal { get; set; }

    public int? GarantiaMeses { get; set; }

    [JsonIgnore]
    public virtual ImagenProducto? ImagenProducto { get; set; }

    [JsonIgnore]
    public virtual ICollection<ComentariosCliente> ComentariosClientes { get; set; } = new List<ComentariosCliente>();

    [JsonIgnore]
    public virtual ICollection<DetallePedidosProveedores> DetallePedidosProveedores { get; set; } = new List<DetallePedidosProveedores>();

    [JsonIgnore]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    [JsonIgnore]
    public virtual ICollection<Garantia> Garantia { get; set; } = new List<Garantia>();

    [JsonIgnore]
    public virtual ICollection<HistorialStock> HistorialStocks { get; set; } = new List<HistorialStock>();

    [JsonIgnore]
    public virtual ICollection<MovimientosInventario> MovimientosInventarios { get; set; } = new List<MovimientosInventario>();

    [JsonIgnore]
    public virtual ICollection<Oferta> Oferta { get; set; } = new List<Oferta>();

    [JsonIgnore]
    public virtual ICollection<ProductosProveedores> ProductosProveedores { get; set; } = new List<ProductosProveedores>();
}
