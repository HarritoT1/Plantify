using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Plantify.Server.Models;

public partial class JardineriaContext : DbContext
{
    public JardineriaContext()
    {
    }

    public JardineriaContext(DbContextOptions<JardineriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Credencial> Credencials { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<GamaProducto> GamaProductos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("cliente");

            entity.HasIndex(e => e.Telefono, "UQ__cliente__2A16D945FA74A478").IsUnique();

            entity.HasIndex(e => e.Fax, "UQ__cliente__D990CC730D26A362").IsUnique();

            entity.HasIndex(e => e.NombreCliente, "idx_nombre_cliente");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApellidoContacto)
                .HasMaxLength(31)
                .IsUnicode(false)
                .HasColumnName("apellido_contacto");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoEmpleadoRepVentas).HasColumnName("codigo_empleado_rep_ventas");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Fax)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("fax");
            entity.Property(e => e.LimiteCredito)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("limite_credito");
            entity.Property(e => e.LineaDireccion1)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("linea_direccion1");
            entity.Property(e => e.LineaDireccion2)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("linea_direccion2");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(31)
                .IsUnicode(false)
                .HasColumnName("nombre_contacto");
            entity.Property(e => e.Pais)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.Region)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("region");
            entity.Property(e => e.Telefono)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Credencial>(entity =>
        {
            entity.ToTable("credencial");

            entity.HasIndex(e => e.Email, "UQ__credenci__AB6E61649BD670CB").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__credenci__F3DBC5728B22FD22").IsUnique();

            entity.HasIndex(e => e.Email, "idx_email");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Pass)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Credencials)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_id_cliente_credencial");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalle___3213E83FFEE62A27");

            entity.ToTable("detalle_pedido");

            entity.HasIndex(e => e.Cantidad, "idx_cantidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
            entity.Property(e => e.IdProducto)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.NumeroLinea).HasColumnName("numero_linea");
            entity.Property(e => e.PrecioUnidad)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("precio_unidad");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(22, 2)")
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_id_pedido");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_id_producto");
        });

        modelBuilder.Entity<GamaProducto>(entity =>
        {
            entity.ToTable("gama_producto");

            entity.Property(e => e.Id)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.DescriptionHtml)
                .IsUnicode(false)
                .HasColumnName("description_html");
            entity.Property(e => e.DescriptionTexto)
                .IsUnicode(false)
                .HasColumnName("description_texto");
            entity.Property(e => e.Imagen)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("imagen");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("pedido");

            entity.HasIndex(e => e.FechaEntrega, "idx_fecha_entrega");

            entity.HasIndex(e => e.FechaEsperada, "idx_fecha_esperada");

            entity.HasIndex(e => e.FechaPedido, "idx_fecha_pedido");

            entity.HasIndex(e => e.NombrePedido, "idx_nombre_pedido");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comentarios)
                .IsUnicode(false)
                .HasColumnName("comentarios");
            entity.Property(e => e.Estado)
                .HasMaxLength(21)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("fecha_entrega");
            entity.Property(e => e.FechaEsperada)
                .HasColumnType("datetime")
                .HasColumnName("fecha_esperada");
            entity.Property(e => e.FechaPedido)
                .HasColumnType("datetime")
                .HasColumnName("fecha_pedido");
            entity.Property(e => e.HiddenClient).HasColumnName("hidden_client");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.NombrePedido)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("nombre_pedido");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_id_cliente_pedido");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("producto");

            entity.HasIndex(e => e.Nombre, "idx_nombre");

            entity.Property(e => e.Id)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CantidadEnStock).HasColumnName("cantidad_en_stock");
            entity.Property(e => e.DescriptionProduct)
                .IsUnicode(false)
                .HasColumnName("description_product");
            entity.Property(e => e.Dimensiones)
                .HasMaxLength(26)
                .IsUnicode(false)
                .HasColumnName("dimensiones");
            entity.Property(e => e.IdGama)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("id_gama");
            entity.Property(e => e.ImagenName)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasDefaultValueSql("('notfound.png')")
                .HasColumnName("imagen_name");
            entity.Property(e => e.Nombre)
                .HasMaxLength(71)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioProveedor)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("precio_proveedor");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("precio_venta");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(51)
                .IsUnicode(false)
                .HasColumnName("proveedor");

            entity.HasOne(d => d.IdGamaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdGama)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_gama_producto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
