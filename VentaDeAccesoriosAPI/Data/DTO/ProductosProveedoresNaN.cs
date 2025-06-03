using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Data.DTO
{
    public class ProductosProveedoresNaN
    {
        public Producto? Producto { get; set; }

        public Proveedores? Proveedor { get; set; }

        public ProductosProveedoresNaN(Producto? producto, Proveedores? proveedor)
        {
            Producto = producto;
            Proveedor = proveedor;
        }
    }
}
