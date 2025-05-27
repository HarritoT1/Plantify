using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantify.Shared
{
    public class PedidoDTO
    {
        public DateTime? FechaPedido { get; set; } 

        public DateTime? FechaEsperada { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public string? Estado { get; set; }

        public string? Comentarios { get; set; }

        public string? NombrePedido { get; set; }

        public bool HiddenClient { get; set; }

        public virtual ICollection<DetallePedidoDTO> DetallePedidos { get; set; } = new List<DetallePedidoDTO>();

        public virtual ClienteDTO? Cliente { get; set; }
    }
}
