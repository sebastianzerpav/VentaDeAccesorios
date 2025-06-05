namespace VentaDeAccesoriosIU.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public decimal? PrecioCompra { get; set; }

        public decimal? PrecioVenta { get; set; }

        public int? StockTotal { get; set; }

        public int? GarantiaMeses { get; set; }

    }

    public class Productos {
        public List<Producto>? productos { get; set; }
    }
}
