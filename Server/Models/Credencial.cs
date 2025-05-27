using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Plantify.Server.Models;

public partial class Credencial
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long? IdCliente { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }
}
