using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Role
{
    public int IdRol { get; set; }

    public string? NombreRol { get; set; }

    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();

    public virtual ICollection<UsuariosRoles> UsuariosRoles { get; set; } = new List<UsuariosRoles>();
}
