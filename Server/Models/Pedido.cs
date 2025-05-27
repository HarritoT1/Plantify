using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Plantify.Server.Models;

public partial class Pedido
{
    public long Id { get; set; }

    public DateTime? FechaPedido { get; set; }

    public DateTime? FechaEsperada { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public string? Estado { get; set; }

    public string? Comentarios { get; set; }

    public long? IdCliente { get; set; }

    public string? NombrePedido { get; set; }

    public bool HiddenClient { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
