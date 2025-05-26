using System;
using System.Collections.Generic;

namespace VentaDeAccesoriosAPI.Data.Models;

public partial class ComentariosCliente
{
    public int IdComentario { get; set; }

    public int? IdCliente { get; set; }

    public int? IdProducto { get; set; }

    public string? Comentario { get; set; }

    public int? Calificacion { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
