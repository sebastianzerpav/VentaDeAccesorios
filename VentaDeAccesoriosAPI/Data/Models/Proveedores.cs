using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Proveedores
{
    public int IdProveedor { get; set; }

    public string? Nombre { get; set; }

    public string? PaisOrigen { get; set; }

    public string? Ciudad { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Tipo { get; set; }

    public virtual ICollection<PedidosProveedores> PedidosProveedores { get; set; } = new List<PedidosProveedores>();

    public virtual ICollection<ProductosProveedores> ProductosProveedores { get; set; } = new List<ProductosProveedores>();
}
