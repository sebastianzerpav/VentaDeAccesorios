using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int? IdCliente { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdSede { get; set; }

    public DateOnly? FechaVenta { get; set; }

    public decimal? TotalVenta { get; set; }

    public string? TipoVenta { get; set; }
    [JsonIgnore]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();
    [JsonIgnore]
    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();
    [JsonIgnore]
    public virtual ICollection<Garantia> Garantia { get; set; } = new List<Garantia>();
    [JsonIgnore]
    public virtual Cliente? IdClienteNavigation { get; set; }
    [JsonIgnore]
    public virtual Sede? IdSedeNavigation { get; set; }
    [JsonIgnore]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<PagosCliente> PagosClientes { get; set; } = new List<PagosCliente>();
    [JsonIgnore]
    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
