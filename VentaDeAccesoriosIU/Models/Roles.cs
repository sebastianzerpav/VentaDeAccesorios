using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


public partial class Role
{
    public int IdRol { get; set; }

    public string? NombreRol { get; set; }
    [JsonIgnore]
    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();
    [JsonIgnore]
    public virtual ICollection<UsuariosRoles> UsuariosRoles { get; set; } = new List<UsuariosRoles>();
}
