using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Transportista
{
    public int IdTransportista { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Empresa { get; set; }

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
