using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? ContrasenaHash { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<PedidosProveedores> PedidosProveedores { get; set; } = new List<PedidosProveedores>();

    public virtual ICollection<UsuariosRoles> UsuariosRoles { get; set; } = new List<UsuariosRoles>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
