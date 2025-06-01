using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Envio
{
    public int IdEnvio { get; set; }

    public int? IdVenta { get; set; }

    public int? IdTransportista { get; set; }

    public string? DireccionEntrega { get; set; }

    public DateOnly? FechaEnvio { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public string? EstadoEnvio { get; set; }
    [JsonIgnore]
    public virtual Transportista? IdTransportistaNavigation { get; set; }
    [JsonIgnore]
    public virtual Venta? IdVentaNavigation { get; set; }
}
