using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;



public partial class ProductosProveedores
{
    public int Id { get; set; }

    public int? IdProducto { get; set; }

    public int? IdProveedor { get; set; }

    public decimal? CostoUnitario { get; set; }

    public int? TiempoEntregaDias { get; set; }
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
    [JsonIgnore]
    public virtual Proveedores? IdProveedorNavigation { get; set; }
}
