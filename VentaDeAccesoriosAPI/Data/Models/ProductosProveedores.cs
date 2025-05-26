using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class ProductosProveedores
{
    public int Id { get; set; }

    public int? IdProducto { get; set; }

    public int? IdProveedor { get; set; }

    public decimal? CostoUnitario { get; set; }

    public int? TiempoEntregaDias { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Proveedores? IdProveedorNavigation { get; set; }
}
