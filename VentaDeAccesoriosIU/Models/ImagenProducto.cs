using System.Text.Json.Serialization;

public class ImagenProducto
{
    public int IdProducto { get; set; }

    public byte[]? Imagen { get; set; }

    public string? NombreArchivo { get; set; }

    public string? TipoContenido { get; set; }

    [JsonIgnore]
    public virtual Producto? Producto { get; set; }
}
