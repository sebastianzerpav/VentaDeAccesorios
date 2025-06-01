using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Oferta
{
    public int IdOferta { get; set; }

    public int? IdProducto { get; set; }

    public decimal? PrecioPromocional { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
}
