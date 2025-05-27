using System;
using System.Collections.Generic;

namespace Plantify.Server.Models;

public partial class GamaProducto
{
    public string Id { get; set; } = null!;

    public string? DescriptionTexto { get; set; }

    public string? DescriptionHtml { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
