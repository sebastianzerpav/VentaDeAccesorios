using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public partial class UsuariosRoles
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdRol { get; set; }
    [JsonIgnore]
    public virtual Role? IdRolNavigation { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
