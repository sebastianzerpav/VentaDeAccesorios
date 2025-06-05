using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;



public partial class Permiso
{
    public int IdPermiso { get; set; }

    public string? NombrePermiso { get; set; }
    [JsonIgnore]
    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();
}
