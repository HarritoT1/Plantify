using System;
using System.Collections.Generic;

namespace Plantify.Server.Models;

public partial class Cliente
{
    public long Id { get; set; }

    //public string NombreCliente { get; set; } = null!;
    public string? NombreCliente { get; set; }

    public string? NombreContacto { get; set; }

    public string? ApellidoContacto { get; set; }

    public string? Telefono { get; set; }

    public string? Fax { get; set; }

    public string? LineaDireccion1 { get; set; }

    public string? LineaDireccion2 { get; set; }

    public string? Ciudad { get; set; }

    public string? Region { get; set; }

    public string? Pais { get; set; }

    public string? CodigoPostal { get; set; }

    public int? CodigoEmpleadoRepVentas { get; set; }

    public decimal? LimiteCredito { get; set; }

    public virtual ICollection<Credencial> Credencials { get; set; } = new List<Credencial>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
