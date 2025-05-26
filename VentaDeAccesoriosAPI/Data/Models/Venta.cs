using System;
using System.Collections.Generic;

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

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual ICollection<Envio> Envios { get; set; } = new List<Envio>();

    public virtual ICollection<Garantia> Garantia { get; set; } = new List<Garantia>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Sede? IdSedeNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<PagosCliente> PagosClientes { get; set; } = new List<PagosCliente>();

    public virtual ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();
}
