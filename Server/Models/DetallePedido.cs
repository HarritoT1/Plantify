using System;
using System.Collections.Generic;

namespace Plantify.Server.Models;

public partial class DetallePedido
{
    public long Id { get; set; }

    public long? IdPedido { get; set; }

    public string? IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal? PrecioUnidad { get; set; }

    public short? NumeroLinea { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
