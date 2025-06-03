using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VentaDeAccesoriosAPI.Data.Models;

namespace VentaDeAccesoriosAPI.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AtencionesGarantia> AtencionesGarantia { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ComentariosCliente> ComentariosClientes { get; set; }

    public virtual DbSet<DetallePedidosProveedores> DetallePedidosProveedores { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<FacturasCompra> FacturasCompras { get; set; }

    public virtual DbSet<Garantia> Garantia { get; set; }

    public virtual DbSet<HistorialStock> HistorialStocks { get; set; }

    public virtual DbSet<MovimientosInventario> MovimientosInventarios { get; set; }

    public virtual DbSet<Oferta> Ofertas { get; set; }

    public virtual DbSet<PagosCliente> PagosClientes { get; set; }

    public virtual DbSet<PagosProveedores> PagosProveedores { get; set; }

    public virtual DbSet<PedidosProveedores> PedidosProveedores { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosProveedores> ProductosProveedores { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }

    public virtual DbSet<Reclamo> Reclamos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<Transportista> Transportistas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosRoles> UsuariosRoles { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }
    public virtual DbSet<ImagenProducto> ImagenProducto { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AtencionesGarantia>(entity =>
        {
            entity.HasKey(e => e.IdAtencion).HasName("PK__Atencion__D0A402366347457D");

            entity.ToTable("Atenciones_Garantia");

            entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");
            entity.Property(e => e.DescripcionSolucion).HasColumnName("descripcion_solucion");
            entity.Property(e => e.FechaAtencion).HasColumnName("fecha_atencion");
            entity.Property(e => e.IdGarantia).HasColumnName("id_garantia");
            entity.Property(e => e.Resultado)
                .HasMaxLength(100)
                .HasColumnName("resultado");

            entity.HasOne(d => d.IdGarantiaNavigation).WithMany(p => p.AtencionesGarantia)
                .HasForeignKey(d => d.IdGarantia)
                .HasConstraintName("FK__Atencione__id_ga__68487DD7");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__677F38F51D8D0801");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(150)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<ComentariosCliente>(entity =>
        {
            entity.HasKey(e => e.IdComentario).HasName("PK__Comentar__1BA6C6F4214BBFFB");

            entity.ToTable("Comentarios_Clientes");

            entity.Property(e => e.IdComentario).HasColumnName("id_comentario");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.Comentario).HasColumnName("comentario");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ComentariosClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Comentari__id_cl__05D8E0BE");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ComentariosClientes)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Comentari__id_pr__06CD04F7");
        });

        modelBuilder.Entity<DetallePedidosProveedores>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Detalle___4F1332DE6357664D");

            entity.ToTable("Detalle_Pedidos_Proveedores");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidosProveedores)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__Detalle_P__id_pe__534D60F1");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidosProveedores)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Detalle_P__id_pr__5441852A");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__Detalle___4F1332DEE75E750F");

            entity.ToTable("Detalle_Ventas");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Detalle_V__id_pr__619B8048");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Detalle_V__id_ve__60A75C0F");
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.IdEnvio).HasName("PK__Envios__8C48C8CA47EEDDBD");

            entity.Property(e => e.IdEnvio).HasColumnName("id_envio");
            entity.Property(e => e.DireccionEntrega)
                .HasMaxLength(200)
                .HasColumnName("direccion_entrega");
            entity.Property(e => e.EstadoEnvio)
                .HasMaxLength(50)
                .HasColumnName("estado_envio");
            entity.Property(e => e.FechaEntrega).HasColumnName("fecha_entrega");
            entity.Property(e => e.FechaEnvio).HasColumnName("fecha_envio");
            entity.Property(e => e.IdTransportista).HasColumnName("id_transportista");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");

            entity.HasOne(d => d.IdTransportistaNavigation).WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdTransportista)
                .HasConstraintName("FK__Envios__id_trans__6E01572D");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Envios__id_venta__6D0D32F4");
        });

        modelBuilder.Entity<FacturasCompra>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Facturas__6C08ED534FE63416");

            entity.ToTable("Facturas_Compra");

            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.FechaFactura).HasColumnName("fecha_factura");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Moneda)
                .HasMaxLength(10)
                .HasColumnName("moneda");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.FacturasCompras)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__Facturas___id_pe__571DF1D5");
        });

        modelBuilder.Entity<Garantia>(entity =>
        {
            entity.HasKey(e => e.IdGarantia).HasName("PK__Garantia__63A683D4247EC893");

            entity.Property(e => e.IdGarantia).HasColumnName("id_garantia");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Garantia)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Garantia__id_pr__656C112C");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Garantia)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Garantia__id_ve__6477ECF3");
        });

        modelBuilder.Entity<HistorialStock>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK__Historia__48155C1F448BE066");

            entity.ToTable("Historial_Stock");

            entity.Property(e => e.IdRegistro).HasColumnName("id_registro");
            entity.Property(e => e.Causa)
                .HasMaxLength(200)
                .HasColumnName("causa");
            entity.Property(e => e.FechaCambio).HasColumnName("fecha_cambio");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdSede).HasColumnName("id_sede");
            entity.Property(e => e.StockActual).HasColumnName("stock_actual");
            entity.Property(e => e.StockAnterior).HasColumnName("stock_anterior");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.HistorialStocks)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Historial__id_pr__75A278F5");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.HistorialStocks)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Historial__id_se__76969D2E");
        });

        modelBuilder.Entity<MovimientosInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__2A071C2462FF6AF4");

            entity.ToTable("Movimientos_Inventario");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdSedeDestino).HasColumnName("id_sede_destino");
            entity.Property(e => e.IdSedeOrigen).HasColumnName("id_sede_origen");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .HasColumnName("motivo");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.MovimientosInventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Movimient__id_pr__70DDC3D8");

            entity.HasOne(d => d.IdSedeDestinoNavigation).WithMany(p => p.MovimientosInventarioIdSedeDestinoNavigations)
                .HasForeignKey(d => d.IdSedeDestino)
                .HasConstraintName("FK__Movimient__id_se__72C60C4A");

            entity.HasOne(d => d.IdSedeOrigenNavigation).WithMany(p => p.MovimientosInventarioIdSedeOrigenNavigations)
                .HasForeignKey(d => d.IdSedeOrigen)
                .HasConstraintName("FK__Movimient__id_se__71D1E811");
        });

        modelBuilder.Entity<Oferta>(entity =>
        {
            entity.HasKey(e => e.IdOferta).HasName("PK__Ofertas__2B7BF92FE75EC358");

            entity.Property(e => e.IdOferta).HasColumnName("id_oferta");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.PrecioPromocional)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precio_promocional");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Oferta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Ofertas__id_prod__02FC7413");
        });

        modelBuilder.Entity<PagosCliente>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pagos_Cl__0941B074EFDF48F9");

            entity.ToTable("Pagos_Clientes");

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.MedioPago)
                .HasMaxLength(50)
                .HasColumnName("medio_pago");
            entity.Property(e => e.MontoPagado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto_pagado");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.PagosClientes)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Pagos_Cli__id_ve__7C4F7684");
        });

        modelBuilder.Entity<PagosProveedores>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pagos_Pr__0941B074736C7272");

            entity.ToTable("Pagos_Proveedores");

            entity.Property(e => e.IdPago).HasColumnName("id_pago");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.IdFactura).HasColumnName("id_factura");
            entity.Property(e => e.MedioPago)
                .HasMaxLength(50)
                .HasColumnName("medio_pago");
            entity.Property(e => e.MontoPagado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto_pagado");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.PagosProveedores)
                .HasForeignKey(d => d.IdFactura)
                .HasConstraintName("FK__Pagos_Pro__id_fa__797309D9");
        });

        modelBuilder.Entity<PedidosProveedores>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedidos___6FF01489C9DF6910");

            entity.ToTable("Pedidos_Proveedores");

            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.EstadoPedido)
                .HasMaxLength(50)
                .HasColumnName("estado_pedido");
            entity.Property(e => e.FechaPedido).HasColumnName("fecha_pedido");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.PedidosProveedores)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Pedidos_P__id_pr__4F7CD00D");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PedidosProveedores)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Pedidos_P__id_us__5070F446");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permisos__228F224F4F289229");

            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(100)
                .HasColumnName("nombre_permiso");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);
            entity.ToTable("Producto");  // Nombre exacto de la tabla en la BD

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Nombre).HasMaxLength(150).HasColumnName("nombre");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.PrecioCompra).HasColumnType("decimal(18,2)").HasColumnName("precio_compra");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(18,2)").HasColumnName("precio_venta");
            entity.Property(e => e.StockTotal).HasColumnName("stock_total");
            entity.Property(e => e.GarantiaMeses).HasColumnName("garantia_meses");

            entity.HasOne(p => p.ImagenProducto)
                .WithOne(ip => ip.Producto)
                .HasForeignKey<ImagenProducto>(ip => ip.IdProducto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Producto_ImagenProducto");
        });
        modelBuilder.Entity<ImagenProducto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);
            entity.ToTable("ImagenProducto"); // Asegúrate que sea así en la BD

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Imagen).HasColumnName("imagen");
            entity.Property(e => e.NombreArchivo).HasMaxLength(255).HasColumnName("nombre_archivo");
            entity.Property(e => e.TipoContenido).HasMaxLength(100).HasColumnName("tipo_contenido");

            entity.HasOne(e => e.Producto)
                .WithOne(p => p.ImagenProducto)
                .HasForeignKey<ImagenProducto>(e => e.IdProducto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ImagenProducto_Producto");
        });



        modelBuilder.Entity<ProductosProveedores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3213E83FA5ED56D9");

            entity.ToTable("Productos_Proveedores");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CostoUnitario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("costo_unitario");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.TiempoEntregaDias).HasColumnName("tiempo_entrega_dias");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Productos__id_pr__4BAC3F29");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Productos__id_pr__4CA06362");
        });

        modelBuilder.Entity<Proveedores>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__8D3DFE28980BE1D1");

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .HasColumnName("ciudad");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.PaisOrigen)
                .HasMaxLength(100)
                .HasColumnName("pais_origen");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Reclamo>(entity =>
        {
            entity.HasKey(e => e.IdReclamo).HasName("PK__Reclamos__C4EA32EE9B6D27B6");

            entity.Property(e => e.IdReclamo).HasColumnName("id_reclamo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.FechaReclamo).HasColumnName("fecha_reclamo");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Reclamos__id_cli__7F2BE32F");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__Reclamos__id_ven__00200768");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__6ABCB5E0475130D4");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(100)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles_Pe__3213E83F7CE92BF7");

            entity.ToTable("Roles_Permisos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPermiso).HasColumnName("id_permiso");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("FK__Roles_Per__id_pe__3F466844");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Roles_Per__id_ro__3E52440B");
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.IdSede).HasName("PK__Sedes__D693504B73721339");

            entity.Property(e => e.IdSede).HasColumnName("id_sede");
            entity.Property(e => e.Barrio)
                .HasMaxLength(100)
                .HasColumnName("barrio");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .HasColumnName("ciudad");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.EmailContacto)
                .HasMaxLength(100)
                .HasColumnName("email_contacto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .HasColumnName("pais");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Transportista>(entity =>
        {
            entity.HasKey(e => e.IdTransportista).HasName("PK__Transpor__6D39A93221D80FC7");

            entity.Property(e => e.IdTransportista).HasColumnName("id_transportista");
            entity.Property(e => e.Empresa)
                .HasMaxLength(100)
                .HasColumnName("empresa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__4E3E04ADD909ED60");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuarios__5B8A068287B1B02A").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.ContrasenaHash)
                .HasMaxLength(255)
                .HasColumnName("contrasena_hash");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(150)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UsuariosRoles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3213E83FFD77FD67");

            entity.ToTable("Usuarios_Roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuarios___id_ro__4316F928");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Usuarios___id_us__4222D4EF");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__459533BFD14098B4");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.FechaVenta).HasColumnName("fecha_venta");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdSede).HasColumnName("id_sede");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.TipoVenta)
                .HasMaxLength(50)
                .HasColumnName("tipo_venta");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("total_venta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Ventas__id_clien__5BE2A6F2");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Ventas__id_sede__5DCAEF64");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Ventas__id_usuar__5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
