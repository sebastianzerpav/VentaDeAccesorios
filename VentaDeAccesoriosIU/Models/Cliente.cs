using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Direccion { get; set; }
    [JsonIgnore]
    public virtual ICollection<ComentariosCliente> ComentariosClientes { get; set; } = new List<ComentariosCliente>();
    [JsonIgnore]
    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
    [JsonIgnore]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
