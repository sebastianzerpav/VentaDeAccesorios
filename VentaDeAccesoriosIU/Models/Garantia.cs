using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;



public partial class Garantia
{
    public int IdGarantia { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? Estado { get; set; }
    [JsonIgnore]
    public virtual ICollection<AtencionesGarantia> AtencionesGarantia { get; set; } = new List<AtencionesGarantia>();
    [JsonIgnore]
    public virtual Producto? IdProductoNavigation { get; set; }
    [JsonIgnore]
    public virtual Venta? IdVentaNavigation { get; set; }
}
