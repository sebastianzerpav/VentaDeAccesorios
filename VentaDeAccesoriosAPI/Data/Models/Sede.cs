using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Sede
{
    public int IdSede { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Barrio { get; set; }

    public string? Ciudad { get; set; }

    public string? Pais { get; set; }

    public string? Telefono { get; set; }

    public string? EmailContacto { get; set; }

    [JsonIgnore]
    public virtual ICollection<HistorialStock> HistorialStocks { get; set; } = new List<HistorialStock>();
    
    [JsonIgnore]
    public virtual ICollection<MovimientosInventario> MovimientosInventarioIdSedeDestinoNavigations { get; set; } = new List<MovimientosInventario>();
   
    [JsonIgnore]
    public virtual ICollection<MovimientosInventario> MovimientosInventarioIdSedeOrigenNavigations { get; set; } = new List<MovimientosInventario>();
    
    [JsonIgnore]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
