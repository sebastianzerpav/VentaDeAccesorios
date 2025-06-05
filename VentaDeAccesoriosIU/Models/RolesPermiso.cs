using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;



public partial class RolesPermiso
{
    public int Id { get; set; }

    public int? IdRol { get; set; }

    public int? IdPermiso { get; set; }
    [JsonIgnore]
    public virtual Permiso? IdPermisoNavigation { get; set; }
    [JsonIgnore]
    public virtual Role? IdRolNavigation { get; set; }
}
